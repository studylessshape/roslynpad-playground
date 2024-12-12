using System.Data;
using System.Linq;

DataTable dataTable = new DataTable();
dataTable.Columns.Add("col1");
dataTable.Columns.Add("col2");
dataTable.Columns.Add("col3");
dataTable.Columns.Add("col4");

var row = dataTable.NewRow();
row["col1"] = "row1-col1";
row["col2"] = 2.2;
row["col3"] = false;
row["col4"] = DateTime.Now;

dataTable.Rows.Add(row);

row = dataTable.NewRow();
row["col1"] = 2;
row["col2"] = 4.4;
row["col3"] = true;
row["col4"] = null;
dataTable.Rows.Add(row);

dataTable.Select()
    .Select(row => new
    {
        Col1 = row["col1"].ToString(),
        Col2 = Convert.ToDecimal(row["col2"]),
        Col3 = Convert.ToBoolean(row["col3"]),
        Col4 = string.IsNullOrEmpty(row["col4"].ToString()) ? (DateTime?)null : DateTime.Parse(row["col4"].ToString()),
    })
    .ToArray()
    .Dump();