#r "nuget: Z.EntityFramework.Extensions.EFCore, 8.103.0"
#r "nuget: Z.EntityFramework.Plus.EFCore, 8.103.0"
using System.Linq;
using System.Linq.Expressions;

var testClasses = new[]
{
    new TestClass { Id = 1 },
    new TestClass { Id = 2 },
    new TestClass { Id = 3 },
    new TestClass { Id = 4 },
    new TestClass { Id = 5 },
    new TestClass { Id = 6 },
    new TestClass { Id = 7 },
    new TestClass { Id = 8 },
};

var ids = new[] {1,3,5};


var query = testClasses.AsQueryable();
var param = Expression.Parameter(typeof(TestClass));
Expression expression = Expression.New(typeof(bool));
foreach (var id in ids)
{
    var newExpression = Expression.Invoke((TestClass p) => p.Id == id, param);
    expression = Expression.OrElse(expression, newExpression);
}

var lambda = Expression.Lambda<Func<TestClass, bool>>(expression, param).Compile();

testClasses.Where(lambda).Dump();

class TestClass
{
    public int Id;
    public bool IsDelete = false;
}