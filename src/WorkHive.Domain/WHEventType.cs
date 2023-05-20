namespace WorkHive.Domain;

[Flags]
public enum WHEventType
{
    None = 0,
    Work = 1,
    Fun = 2,
    Online = 4,
    Babysitting = 8,
    WorkAndFun = Work | Fun
}