namespace WorkHive.Core;

[Flags]
public enum WHEventType
{
    None = 0,
    Work = 1,
    Fun = 2,
    WorkAndFun = Work | Fun
}