using System.Diagnostics.CodeAnalysis;
using System.Linq;

List<Data> datas =
[
    new Data() { Id = 1, Name = "Root" },
    new Data() { Id = 2, ParentId = 1, Name = "Home" },
    new Data() { Id = 3, ParentId = 1, Name = "Manage" },
    new Data() { Id = 4, ParentId = 3, Name = "Module" },
    new Data() { Id = 5, ParentId = 3, Name = "Role" },
    new Data() { Id = 6, ParentId = 3, Name = "User", Order = -1 }
];
datas.Sort((d1, d2) =>
{
    if (d1.Order == d2.Order) {
        return d1.Id < d2.Id ? -1 : 1;
    }
    return d1.Order < d2.Order ? -1 : 1;
});

var nodes = new TreeIter<Data, DataNode>(datas, (node, data) => node.Id == data.ParentId, (data) => new DataNode(data)).BuildTree().Dump();

nodes[0].Children[1].Dump();



interface IChildren<T> {
    List<T> Children { get; }
}

class Data {
    public int Id { get; set; }
    public int? ParentId { get; set; }
    public string Name {get;set;} = "";
    public int Order { get; set; }

    public override string ToString()
    {
        return Name;
    }
}

class DataNode(Data data) : IChildren<DataNode> {
    public Data Data { get; } = data;
    public List<DataNode> Children { get; } = [];
    
     public override string ToString()
    {
        return $"Data: {Data!.ToString()}";
    }
}

class DataNode<T, R>([NotNull] T data, R node) {

    public T Data { get; set; } = data;
    public R Node { get; set; } = node;

    public override string ToString()
    {
        return $"Data: {Data!.ToString()}";
    }
}

class TreeIter<T, R>
    where R: IChildren<R>
{
    private List<DataNode<T, R>> dataNodes;
    private readonly Func<T, T, bool> childrenFilter;

    public TreeIter(List<T> datas, Func<T,T,bool> childrenFilter, Func<T, R> dataToR)
    {
        dataNodes = datas.Select(d => new DataNode<T, R>(d, dataToR(d))).ToList();
        this.childrenFilter = childrenFilter;
    }
    
    public R[] BuildTree()
    {
        for (int index = 0; index < dataNodes.Count; index++)
        {
            var node = dataNodes[index];
            BuildChildren(node);
            var currentIndex = dataNodes.FindIndex(dn => dn.Data.Equals(node.Data));
            if(currentIndex >= 0)
            {
                index = currentIndex;
            }
        }
        return dataNodes.Select(dn => dn.Node).ToArray();
    }
    
    private DataNode<T, R> BuildChildren(DataNode<T,R> node)
    {
        var children = dataNodes.Where(dn => childrenFilter(node.Data, dn.Data)).ToList();
        dataNodes = dataNodes.Except(children).ToList();
        foreach (var child in children)
        {
            var childNode = BuildChildren(child);
            node.Node.Children.Add(childNode.Node);
        }
        return node;
    }
}