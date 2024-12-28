using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Wpf.Ui;
using Wpf.Ui.Controls;
using Wpf.Ui.Extensions;

namespace WPFUI_SAMPLE.Helpers;

public static class ObservableValidatorExtensions
{

    public static async Task<bool> ValidateObjectWithDialogAsync(this ObservableValidator validator,
        IContentDialogService contentDialogService, object instance)
    {
        if (instance == null)
        {
            return false;
        }
        var validationContext = new ValidationContext(instance);
        var validationResults = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(instance, validationContext, validationResults, true);

        if (!isValid)
        {
            var errorMessage = string.Join("\n", validationResults.Select(vr => vr.ErrorMessage));
            //MessageBox.Show(errorMessage, "验证错误", MessageBoxButton.OK, MessageBoxImage.Error);
            ContentDialogResult result = await contentDialogService.ShowSimpleDialogAsync(
            new SimpleContentDialogCreateOptions()
            {
                Title = "验证错误",
                Content = errorMessage,
                PrimaryButtonText = "确定",
                CloseButtonText = "取消"
            }
        );

            /*DialogResultText = result switch
            {
                ContentDialogResult.Primary => "User saved their work",
                ContentDialogResult.Secondary => "User did not save their work",
                _ => "User cancelled the dialog"
            };*/
        }

        // 输出调试信息
        Debug.WriteLine($"验证结果: {isValid}");
        foreach (var result in validationResults)
        {
            Debug.WriteLine($"错误信息: {result.ErrorMessage}");
        }

        return isValid;
    }


}
