using System.ComponentModel.DataAnnotations;

namespace StudioStatistic.Client.Validation
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is not DateTime dateTime)
                return false;

            return dateTime > DateTime.Now;
        }

        public override string FormatErrorMessage(string name)
        {
            return ErrorMessage ?? "Дата визита должна быть в будущем";
        }
    }
}