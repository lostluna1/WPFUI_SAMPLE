// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using Wpf.Ui.Abstractions.Controls;

namespace WPFUI_SAMPLE.ViewModels;

public abstract partial class ViewModel : ObservableValidator, INavigationAware
{
    /// <inheritdoc />
    public async virtual Task OnNavigatedToAsync()
    {
        using CancellationTokenSource cts = new();

        await DispatchAsync(OnNavigatedTo, cts.Token);
    }

    /// <summary>
    /// Handles the event that is fired after the component is navigated to.
    /// </summary>
    // ReSharper disable once MemberCanBeProtected.Global
    public virtual void OnNavigatedTo()
    {
    }

    /// <inheritdoc />
    public async virtual Task OnNavigatedFromAsync()
    {
        using CancellationTokenSource cts = new();

        await DispatchAsync(OnNavigatedFrom, cts.Token);
    }

    /// <summary>
    /// Handles the event that is fired before the component is navigated from.
    /// </summary>
    // ReSharper disable once MemberCanBeProtected.Global
    public virtual void OnNavigatedFrom()
    {
    }

    /// <summary>
    /// Dispatches the specified action on the UI thread.
    /// </summary>
    /// <param name="action">The action to be dispatched.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    protected static async Task DispatchAsync(Action action, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return;
        }

        await Application.Current.Dispatcher.InvokeAsync(action);
    }
}
