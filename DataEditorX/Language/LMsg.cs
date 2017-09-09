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
		titleInfo = 0 ,
		titleError = 0x1,
		titleWarning = 0x2,
		titleQuestion = 0x3,

		CreateSucceed = 0x4,
		CreateFail = 0x5,
		AddSucceed = 0x6,
		AddFail = 0x7,

		CodeCanNotIsZero = 0x8,
		ItIsExists = 0x9,

		ItIsNotChanged = 0xa,
		IfDeleteCard = 0xb,
		IfCreateScript = 0xc,
		IfOpenDataBase = 0xd,
		IfReplaceExistingCard = 0xe,
		NowIsNewVersion = 0xf,

		CheckUpdateFail = 0x10,
		HaveNewVersion = 0x11,
		FileIsNotExists = 0x12,
		NotSelectDataBase = 0x13,
		SelectDataBasePath = 0x14,
		SelectYdkPath = 0x15,
		SelectImagePath = 0x16,
		DownloadSucceed = 0x17,
		DownloadFail = 0x18,
		NotSelectScriptText = 0x19,
		DeleteSucceed = 0x1a,
		DeleteFail = 0x1b,
		ModifySucceed = 0x1c,
		ModifyFail = 0x1d,
		About = 0x1e,
		Version = 0x1f,
		Author = 0x20,
		CdbType = 0x21,
		ydkType = 0x22,
		Setcode_error = 0x23,
		SelectImage = 0x24,
		ImageType =0x25,
		RunError = 0x26,
		checkUpdate = 0x27,
		CopyCardsToDB = 0x28,
		CopyCardsToDBIsOK =0x29,
		selectMseset = 0x2a,
		MseType = 0x2b,
		SaveMse = 0x2c,
		SaveMseOK = 0x2d,
		CutImage = 0x2e,
		CutImageOK = 0x2f,

		NoSelectCard = 0x30,
		IfReplaceExistingImage = 0x31,
		ConvertImage = 0x32,
		ConvertImageOK = 0x33,
		CompDBOK = 0x34,
		OnlySet = 0x35,
		CancelTask = 0x36,
		PauseTask = 0x37,
		ResumeTask = 0x38,
		TaskError = 0x39,
		IfCancelTask = 0x3a,
		CopyCards = 0x3b,
		PasteCards = 0x3c,
		ClearHistory = 0x3d,
		ExportData = 0x3e,
		ExportDataOK = 0x3f,

		CheckText = 0x40,
		CompareOK = 0x41,
		OpenFile = 0x42,
		ScriptFilter =0x43,
		NewFile = 0x44,
		SaveFileOK = 0x45,
		IfSaveScript =0x46,
		ReadMSE = 0x47,
		ReadMSEisOK = 0x48,

		PlzRestart = 0x49,
		exportMseImages = 0x4a,
		exportMseImagesErr = 0x4b,
		COUNT,
	}
}
