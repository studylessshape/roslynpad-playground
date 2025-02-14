var sets = new List<SingleSet>()
{
    new SingleSet()
    {
        En1 = En1.L1,
        En2 = En2.S1,
        Cond1 = true,
        Cond2 = false,
    },
    new SingleSet()
    {
        En1 = En1.L1,
        En2 = En2.S2,
        Cond1 = true,
        Cond2 = false,
    },
    new SingleSet()
    {
        En1 = En1.L1,
        En2 = En2.S3,
        Cond1 = true,
        Cond2 = false,
    },
    new SingleSet()
    {
        En1 = En1.L1,
        En2 = En2.S4,
        Cond1 = true,
        Cond2 = false,
    },
    new SingleSet()
    {
        En1 = En1.L1,
        En2 = En2.S1,
        Cond1 = false,
        Cond2 = false,
    },
};

sets.DistinctBy(s => (s.En1, s.En2)).ToList().Dump();

enum En1
{
    L1,
    L2
}

enum En2
{
    S1,
    S2,
    S3,
    S4
}

class SingleSet
{
    public En1 En1 {get;set;}
    public En2 En2 {get;set;}
    public bool Cond1 {get;set;}
    public bool Cond2 {get;set;}
}