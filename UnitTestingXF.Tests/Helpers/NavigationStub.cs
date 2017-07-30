using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace UnitTestingXF.Tests.Helpers
{
    public class NavigationStub : INavigation
    {
        public Page CurrentPage { get; private set; }

        public Page CurrentModalPage { get; private set; }

        public IReadOnlyList<Page> ModalStack => throw new NotImplementedException();

        public IReadOnlyList<Page> NavigationStack => throw new NotImplementedException();

        public void InsertPageBefore(Page page, Page before)
        {
            throw new NotImplementedException();
        }

        public Task<Page> PopAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Page> PopAsync(bool animated)
        {
            throw new NotImplementedException();
        }

        public Task<Page> PopModalAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Page> PopModalAsync(bool animated)
        {
            throw new NotImplementedException();
        }

        public Task PopToRootAsync()
        {
            throw new NotImplementedException();
        }

        public Task PopToRootAsync(bool animated)
        {
            throw new NotImplementedException();
        }

        public async Task PushAsync(Page page)
        {
            await Task.FromResult(CurrentPage = page);
        }

        public async Task PushAsync(Page page, bool animated)
        {
            await Task.FromResult(CurrentPage = page);
        }

        public async Task PushModalAsync(Page page)
        {
            await Task.FromResult(CurrentModalPage = page);
        }

        public async Task PushModalAsync(Page page, bool animated)
        {
            await Task.FromResult(CurrentModalPage = page);
        }

        public void RemovePage(Page page)
        {
            throw new NotImplementedException();
        }
    }
}
