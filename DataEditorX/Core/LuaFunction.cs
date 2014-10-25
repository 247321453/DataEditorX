/*
 * 由SharpDevelop创建。
 * 用户： Acer
 * 日期: 2014-10-25
 * 时间: 8:12
 * 
 */
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DataEditorX
{
	/// <summary>
	/// Description of LuaFunction.
	/// </summary>
	public class LuaFunction
	{
		#region log
		static string oldfun;
		static string logtxt;
		static string funclisttxt;
		static void ResetLog(string file)
		{
			File.Delete(logtxt);
		}
		static void Log(string str)
		{
			File.AppendAllText(logtxt, str+Environment.NewLine);
		}
		#endregion
		
		#region old functions
		static SortedList<string,string> funclist=new SortedList<string,string>();
		public static SortedList<string,string> Read(string funtxt)
		{
			funclist.Clear();
			oldfun=funtxt;
			SortedList<string,string> list=new SortedList<string,string>();
			if(File.Exists(funtxt))
			{
				string[] lines=File.ReadAllLines(funtxt);
				bool isFind=false;
				string name="";
				string desc="";
				foreach(string line in lines)
				{
					if(string.IsNullOrEmpty(line)
					   || line.StartsWith("==")
					   || line.StartsWith("#"))
						continue;
					if(line.StartsWith("●"))
					{
						//add
						if(!string.IsNullOrEmpty(name))
						{
							if(list.ContainsKey(name)){
								list[name] +=Environment.NewLine+desc;
								funclist[name] +=Environment.NewLine+desc;
							}
							else{
								list.Add(name, desc);
								funclist.Add(name, desc);
							}
						}
						int w=line.IndexOf("(");
						int t=line.IndexOf(" ");
						
						if(t<w && t>0){
							name=line.Substring(t+1,w-t-1);
							isFind=true;
							desc=line.Replace("●","");
						}
					}
					else if(isFind){
						desc+=Environment.NewLine+line;
					}
				}
				if(!string.IsNullOrEmpty(name))
				{
					if(list.ContainsKey(name)){
						list[name] +=Environment.NewLine+desc;
						funclist[name] +=Environment.NewLine+desc;
					}
					else{
						list.Add(name, desc);
						funclist.Add(name, desc);
					}
				}
			}
			return list;
		}
		#endregion
		
		static void Save()
		{
			if(string.IsNullOrEmpty(oldfun))
				return;
			using(FileStream fs=new FileStream(oldfun+"_sort.txt",
			                                   FileMode.Create,
			                                   FileAccess.Write))
			{
				StreamWriter sw=new StreamWriter(fs,Encoding.UTF8);
				foreach(string k in funclist.Keys)
				{
					sw.WriteLine("●"+funclist[k]);
				}
				sw.Close();
			}

		}
		#region find libs
		public static bool Find(string path)
		{
			string name="interpreter.cpp";
			string file=Path.Combine(path,name);
			string file2=Path.Combine(Path.Combine(path, "ocgcore"), name);
			logtxt=Path.Combine(path, "find_functions.log");
			ResetLog(logtxt);
			funclisttxt =Path.Combine(path, "_functions.txt");
			File.Delete(funclisttxt);
			if(!File.Exists(file)){
				Log("error: no find file "+file);
				if(File.Exists(file2)){
					file=file2;
					path=Path.Combine(path, "ocgcore");
				}
				else{
					Log("error: no find file "+file2);
					return false;
				}
			}
			string texts=File.ReadAllText(file);
			Regex libRex=new Regex(@"\sluaL_Reg\s([a-z]*?)lib\[\]([\s\S]*?)^\}"
			                       ,RegexOptions.Multiline);
			MatchCollection libsMatch=libRex.Matches(texts);
			Log("log:count "+libsMatch.Count.ToString());
			foreach ( Match m in libsMatch )
			{
				if(m.Groups.Count>2){
					string word=m.Groups[1].Value;
					Log("log:find "+word);
					GetFunctions(word, m.Groups[2].Value,
					             Path.Combine(path,"lib"+word+".cpp"));
				}
			}
			Save();
			return true;
		}
		#endregion
		
		#region find function name
		static string ToTitle(string str)
		{
			return str.Substring(0, 1).ToUpper()+str.Substring(1);
		}
		static Dictionary<string,string> GetFunctionNames(string texts,string name)
		{
			Dictionary<string,string> dic=new Dictionary<string, string>();
			Regex funcRex=new Regex("\"(\\S*?)\",\\s*?(\\S*?::\\S*?)\\s");
			MatchCollection funcsMatch=funcRex.Matches(texts);
			Log("log: functions count "+name+":"+funcsMatch.Count.ToString());
			foreach ( Match m in funcsMatch )
			{
				if(m.Groups.Count>2)
				{
					string k=ToTitle(name)+"."+m.Groups[1].Value;
					string v=m.Groups[2].Value;
					if(!dic.ContainsKey(k))
						dic.Add(k,v);
				}
			}
			return dic;
		}
		#endregion
		
		#region find code
		static string FindCode(string texts,string name)
		{
			Regex reg=new Regex(@"int32\s+?"+name
			                    +@"[\s\S]+?\{([\s\S]*?^)\}",
			                    RegexOptions.Multiline);
			Match mc=reg.Match(texts);
			if(mc.Success)
			{
				if(mc.Groups.Count>1)
					return mc.Groups[0].Value
						.Replace("\r\n","\n")
						.Replace("\r","\n")
						.Replace("\n",Environment.NewLine);
			}
			return "";
		}
		#endregion
		
		#region find return
		static string FindReturn(string texts,string name)
		{
			string restr="";
			if(texts.IndexOf("lua_pushboolean")>=0)
				return "bool ";
			else
			{
				if(texts.IndexOf("interpreter::card2value")>=0)
					restr += "Card ";
				if(texts.IndexOf("interpreter::group2value")>=0)
					restr += "Group ";
				if(texts.IndexOf("interpreter::effect2value")>=0)
					restr += "Effect ";
				else if(texts.IndexOf("interpreter::function2value")>=0)
					restr += "function ";
				
				if(texts.IndexOf("lua_pushinteger")>=0)
					restr += "int ";
				if(texts.IndexOf("lua_pushstring")>=0)
					restr += "string ";
			}
			if(string.IsNullOrEmpty(restr))
				restr="void ";
			if(restr.IndexOf(" ") !=restr.Length-1){
				restr = restr.Replace(" ","|").Substring(0,restr.Length-1)+" ";
			}
			return restr;
		}
		#endregion
		
		#region find args
		static string getUserType(string str)
		{
			if(str.IndexOf("card")>=0)
				return "Card";
			if(str.IndexOf("effect")>=0)
				return "Effect";
			if(str.IndexOf("group")>=0)
				return "Group";
			
			return "Any";
		}
		
		static void AddArgs(string texts,string regx,string arg,SortedList<int,string> dic)
		{
			//function
			Regex reg=new Regex(regx);
			MatchCollection mcs=reg.Matches(texts);
			foreach(Match m in mcs)
			{
				if(m.Groups.Count>1)
				{
					string v=arg;
					int k=int.Parse(m.Groups[1].Value);
					if(dic.ContainsKey(k))
						dic[k] = dic[k]+"|"+v;
					else
						dic.Add(k,v);
				}
			}
		}
		static string FindArgs(string texts,string name)
		{
			SortedList<int,string> dic=new SortedList<int, string>();
			//card effect ggroup
			Regex reg=new Regex(@"\((\S+?)\)\s+?lua_touserdata\(L,\s+(\d+)\)");
			MatchCollection mcs=reg.Matches(texts);
			foreach(Match m in mcs)
			{
				if(m.Groups.Count>2)
				{
					string v=m.Groups[1].Value.ToLower();
					v = getUserType(v);
					int k=int.Parse(m.Groups[2].Value);
					if(dic.ContainsKey(k))
						dic[k] = dic[k]+"|"+v;
					else
						dic.Add(k,v);
				}
			}
			//function
			AddArgs(texts
			        ,@"interpreter::get_function_handle\(L,\s+(\d+)\)"
			        ,"function",dic);
			//int
			AddArgs(texts,@"lua_tointeger\(L,\s+(\d+)\)","integer",dic);
			//string
			AddArgs(texts,@"lua_tostring\(L,\s+(\d+)\)","string",dic);
			//bool
			AddArgs(texts,@"lua_toboolean\(L,\s+(\d+)\)","boolean",dic);

			string args="(";
			foreach(int i in dic.Keys)
			{
				args +=dic[i]+", ";
			}
			if(args.Length>1)
				args = args.Substring(0,args.Length-2);
			args += ")";
			return args;
		}
		#endregion
		
		#region find old
		static string FindOldDesc(string name)
		{
			if(funclist.ContainsKey(name))
				return funclist[name];
			return "";
		}
		#endregion
		
		#region Save Functions
		public static void GetFunctions(string name,string texts,string file)
		{
			if(!File.Exists(file)){
				Log("error:no find file "+file);
				return;
			}
			string cpps=File.ReadAllText(file);
			//lua name /cpp name
			Dictionary<string,string> fun=GetFunctionNames(texts,name);
			if(fun==null || fun.Count==0){
				Log("warning: no find functions of "+name);
				return;
			}
			Log("log: find functions "+name+":"+fun.Count.ToString());

			using(FileStream fs=new FileStream(file+".txt",
			                                   FileMode.Create,
			                                   FileAccess.Write))
			{
				StreamWriter sw=new StreamWriter(fs, Encoding.UTF8);
				sw.WriteLine("========== "+name+" ==========");
				File.AppendAllText(funclisttxt, "========== "+name+" ==========");
				foreach(string k in fun.Keys)
				{
					string v=fun[k];
					string code=FindCode(cpps, v);
					string txt="●"+FindReturn(code,v)+k+FindArgs(code,v)
						+Environment.NewLine
						+FindOldDesc(k)
						+Environment.NewLine
						+code;
					sw.WriteLine(txt);
					
					File.AppendAllText(funclisttxt,txt+Environment.NewLine);
				}
				sw.Close();
			}
		}
		#endregion
	}
}
