#r "nuget: Microsoft.Extensions.Configuration, 8.0.0"
#r "nuget: Microsoft.Extensions.DependencyInjection, 8.0.0"
#r "nuget: Microsoft.Extensions.Hosting, 8.0.0"
#r "nuget: Microsoft.EntityFrameworkCore, 8.0.6"
#r "nuget: Pomelo.EntityFrameworkCore.MySql, 8.0.2"

using Microsoft.Extensions.Hosting;

IHost? host = null;

void ChangeClass(CanChange change)
{
    change.Change = 10;
    change.StringChange = "123";
    change.Child = new()
    {
        Change = 10,
        StringChange = "111",
    };
}

CanChange change = new CanChange();
change.Child = new();

change.Dump("Before");

ChangeClass(change);
change.Dump("after");

class CanChange
{
    public int Change {get;set;}
    public string StringChange {get;set;}
    public ChildChange Child {get;set;}
    public class ChildChange
    {
        public int Change {get;set;}
        public string StringChange {get;set;}
    }
}