/*
 * 由SharpDevelop创建。
 * 用户： Acer
 * 日期: 7月8 星期二
 * 时间: 10:21
 * 
 */
using System;

namespace DataEditorX.Language
{
    public enum LMSG : uint
    {
        titleInfo=0,
        titleError,
        titleWarning,
        titleQuestion,
        
        CreateSucceed,
        CreateFail,
        AddSucceed,
        AddFail,

        CodeCanNotIsZero,
        ItIsExists,

        ItIsNotChanged,
        IfDeleteCard,
        IfCreateScript,
        IfOpenDataBase,
        IfReplaceExistingCard,
        NowIsNewVersion,
        CheckUpdateFail,
        HaveNewVersion,
        FileIsNotExists,
        NotSelectDataBase,
        SelectDataBasePath,
        SelectYdkPath,
        SelectImagePath,
        DownloadSucceed,
        DownloadFail,
        NotSelectScriptText,
        DeleteSucceed,
        DeleteFail,
        ModifySucceed,
        ModifyFail,
        About,
        Version,
        Author,
        CdbType,
        ydkType,
        COUNT,
    }
}
