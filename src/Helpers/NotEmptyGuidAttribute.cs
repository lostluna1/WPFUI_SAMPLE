using System.ComponentModel.DataAnnotations;

public class NotEmptyGuidAttribute : ValidationAttribute
{
    public NotEmptyGuidAttribute() : base("Guid 不能为空或为 Guid.Empty")
    {
    }

    public override bool IsValid(object? value)
    {
        if (value is Guid guid)
        {
            return guid != Guid.Empty;
        }
        return false;
    }
}
