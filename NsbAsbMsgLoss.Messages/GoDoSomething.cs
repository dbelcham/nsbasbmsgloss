namespace NsbAsbMsgLoss.Messages;

public class GoDoSomething
{
    public Guid Value1 { get; set; }
    public DateTime Value2 { get; set; }
    public long Value3 { get; set; }
    public long Value4 { get; set; }
    public long Value5 { get; set; }
    public bool Value6 { get; set; }
    public long Value7 { get; set; }
    public long Value8 { get; set; }
    public bool Value9 { get; set; }
    public List<GoDoSomethingSubType> Value10 { get; set; }
    public long Id { get; set; }
}

public class GoDoSomethingSubType
{
    public long Value1 { get; set; }
    public long Value2 { get; set; }
    public long Value3 { get; set; }
    public long Value4 { get; set; }
    public long Value5 { get; set; }
    public DateTime Value6 { get; set; }
    public long Value7 { get; set; }
}