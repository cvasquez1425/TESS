using System.Data;
using System.Text;
using Greenspoon.Tess.DataObjects.AdoNet;
using System;

namespace Greenspoon.Tess.Services
{
    public class ScannedImageService
    {
        [Obsolete("Use overloaded method with project id.")]
        public static DataTable GetScannedImage(int contractId)
        {
            var sb = new StringBuilder();
            sb.AppendLine(@"DECLARE @DEFSERVER VARCHAR (50)");
            sb.AppendLine(@"DECLARE @Contract VARCHAR(10)");
            sb.AppendLine(string.Format("SET @Contract ='{0}'", contractId));
            sb.AppendLine(@"SELECT @DEFSERVER=location FROM MHGROUP.DOCSERVERS WHERE DOCSERVER='DEFSERVER'");
            sb.AppendLine(@"SELECT docnum,docname, T_Alias AS doctype, REPLACE(docloc,'DEFSERVER:',@DEFSERVER) AS 'DocLocation'");
            sb.AppendLine(@"FROM MHGROUP.DOCMASTER");
            sb.AppendLine(@"WHERE C1ALIAS='GMPRACTICEAREAS'");
            sb.AppendLine(@"AND C2ALIAS='ESCROW'");
            sb.AppendLine(@"AND SUBSTRING(substring(DOCNAME,(PATINDEX('%[_]%',DOCNAME)+1),50),1,(PATINDEX('%[_]%',substring(DOCNAME,(PATINDEX('%[_]%',DOCNAME)+1),50))-1))=@Contract");
            sb.AppendLine(@"AND TYPE='D'");

            return Db.GetDataTable(sb.ToString(), "img");
        }

        public static DataTable GetScannedImage(int contractId, string projectId)
        {
            var sb = new StringBuilder();
            sb.AppendLine(@"DECLARE @Project VARCHAR (5)");
            sb.AppendLine(@"DECLARE @DEFSERVER VARCHAR (50)");
            sb.AppendLine(@"DECLARE @Contract VARCHAR(10)");
            sb.AppendLine(string.Format("SET @Contract ='{0}'", contractId));
            sb.AppendLine(string.Format("SET @Project ='{0}'", projectId));
            sb.AppendLine(@"SELECT @DEFSERVER=location FROM MHGROUP.DOCSERVERS WHERE DOCSERVER='DEFSERVER'");
            sb.AppendLine(@"SELECT docnum,docname, T_Alias AS doctype, REPLACE(docloc,'DEFSERVER:',@DEFSERVER) AS 'DocLocation'");
            sb.AppendLine(@"FROM MHGROUP.DOCMASTER");
            sb.AppendLine(@"WHERE C1ALIAS='TESS'");
            sb.AppendLine(@"AND C2ALIAS=@Project");
            sb.AppendLine(@"AND DOCNAME LIKE '%'+'[_]'+@Contract+'[_]'+'%'");
            sb.AppendLine(@"AND TYPE='D'");

            return Db.GetDataTable(sb.ToString(), "img");
        }
    }
}