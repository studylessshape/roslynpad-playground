using System.Data.OleDb;

try
{
    string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + @"D:\Projects\soft\svn\ZC_WMS\SZ_ZC_WMS\文档\增补需求\WMS立库库存明细 (7).xlsx" + ";Extended Properties='Excel 12.0; HDR=YES; IMEX=1'";
    String strExcel = "SELECT * FROM  [原料$]";
    System.Data.DataTable items = new System.Data.DataTable();
    using (OleDbConnection OleConn = new OleDbConnection(strConn))
    {
        OleConn.Open();
        OleDbDataAdapter adapter = new OleDbDataAdapter(strExcel, OleConn);
        adapter.Fill(items);
    }
}
catch (Exception err)
{
    err.Dump();
}