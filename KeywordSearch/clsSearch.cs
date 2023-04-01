using System;
using System.Collections.Generic;
using System.IO;

namespace KeywordSearch
{
    class clsSearch
    {
        //To store the output data such as FileName, LineNumber, and LineText
        public struct sKeyword
        {
            public string strFileName;
            public int LineNumber;
            public string strLineText;
        }

        #region SearchKeyword
        //Main function to search keyword from a text file in a specified directory
        //Output data will be stored in an arraylist of struct
        public static bool SearchKeyword(string strDirectory, string strKeyword, out List<sKeyword> lstKeyword, out string strErrMsg)
        {
            strErrMsg = string.Empty;
            lstKeyword = new List<sKeyword>();

            string[] strFile = null;
            bool blnRet = true;

            try
            {
                if (GetTextFiles(strDirectory, out strFile, out strErrMsg))
                {
                    for (int i = 0; i < strFile.Length; i++)
                    {
                        if (!CheckKeywordExistence(strKeyword, strFile[i], ref lstKeyword, out strErrMsg))
                        {
                            blnRet = false;
                            break;
                        }
                    }

                    if (lstKeyword.Count == 0)
                    {
                        blnRet = false;
                        strErrMsg = "There is no keyword: '" + strKeyword + "' found in the directory";
                    }
                }
                else
                    blnRet = false;
            }
            catch (Exception ex)
            {
                blnRet = false;
                strErrMsg = "Exception: " + ex.Message;
            }

            return blnRet;
        }
        #endregion

        #region GetTextFiles
        //To check and return the full filepath in a directory which contain text file
        //Error will be thrown if it is invalid path or there is no text file in a specified directory
        private static bool GetTextFiles(string strDirectory, out string[] strFile, out string strErrMsg)
        {
            strFile = null;
            strErrMsg = string.Empty;

            bool blnRet = false;

            try
            {
                if (Directory.Exists(strDirectory))
                {
                    strFile = Directory.GetFiles(strDirectory, "*.txt", SearchOption.AllDirectories);

                    if (strFile.Length > 0)
                        blnRet = true;
                    else
                        strErrMsg = "There is no text file found at the specified directory";
                }
                else
                    strErrMsg = "Invalid file directory";
            }
            catch (Exception ex)
            {
                blnRet = false;
                strErrMsg = "Exception: " + ex.Message;
            }
            return blnRet;
        }
        #endregion

        #region CheckKeywordExistence
        //To check whether a keyword was existed in a text file
        //If keyword exist, save data (FileName, LineNumber, LineText) into an arraylist of struct
        private static bool CheckKeywordExistence(string strKeyword, string strFile, ref List<sKeyword> lstKeyword, out string strErrMsg)
        {
            strErrMsg = string.Empty;

            string[] strText = null;
            bool blnRet = true;
            sKeyword sKey = new sKeyword();

            try
            {
                strText = File.ReadAllLines(strFile);

                for (int i=0; i<strText.Length; i++)
                {
                    strText[i] = strText[i].Trim(' ');

                    if (strText[i].IndexOf(strKeyword, 0, StringComparison.OrdinalIgnoreCase) > -1)
                    {
                        sKey.strFileName = strFile;
                        sKey.LineNumber = i + 1;
                        sKey.strLineText = strText[i];
                        lstKeyword.Add(sKey);
                    }
                }
            }
            catch (Exception ex)
            {
                blnRet = false;
                lstKeyword.Clear();
                strErrMsg = "Exception: " + ex.Message;
            }

            return blnRet;
        }
        #endregion
    }
}
