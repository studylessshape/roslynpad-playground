Request<TestType> req = new();

(typeof(TestType) == req.Type).Dump();

class TestType {}

class Request<T>
{
    public Type Type => typeof(T);
}