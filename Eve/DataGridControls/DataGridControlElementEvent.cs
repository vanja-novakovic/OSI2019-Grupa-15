using Core.Common;
using Core.Services.Interfaces;
using Database.Entities;
using Eve.AutoMapper;
using Eve.Shared.Config;
using Eve.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Eve.DataGridControls
{
    public sealed class DataGridControlElementEvent
    {
        private readonly IEventService eventService = ServicesFactory.GetInstance().CreateIEventService();

        public static int FIRST_NUMBER = 0;
        public IList ListForPage { get; set; }
        public int TotalNumberOfItems { get; set; }

        public string OrderByAttribute { get; set; }

        public EventFilter Filter { get; set; } = EventFilter.NONE;

        public int? IdCategory { get; set; } = null;

        public int NumberOfRecordsPerPage { get; set; } = Shared.Config.Properties.Default.NumberOfRecordsPerPage;
        public int NextNumber { get; set; } = 0;

        public DataGrid DataGrid { get; set; }
        public Label PageInfo { get; set; }

        private Pager PagedTable;


        public DataGridControlElementEvent(DataGrid dataGrid, Label pageInfo, int totalNumberOfItems)
        {
            TotalNumberOfItems = totalNumberOfItems;
            DataGrid = dataGrid;
            PageInfo = pageInfo;

        }

        public DataGridControlElementEvent()
        {
            SetTotalNumberOfItems();
            SetList();
        }

        public async void SetList()
        {
            ListForPage = await GetData(FIRST_NUMBER, NumberOfRecordsPerPage, Filter, IdCategory, OrderByAttribute);
        }
        public async void SetTotalNumberOfItems()
        {
            TotalNumberOfItems = await GetNumberOfItems();
            if (TotalNumberOfItems < NumberOfRecordsPerPage)
            {
                NumberOfRecordsPerPage = TotalNumberOfItems;
            }
        }
        public async Task Show()
        {
            PagedTable = new Pager() { DataType = GetDataType(), TotalNumberToDisplay = TotalNumberOfItems };
            await SetFields(FIRST_NUMBER);
            SetPage(PagedTable.First(ListForPage, NumberOfRecordsPerPage).DefaultView);
        }
        public async Task Last_Click(object sender, RoutedEventArgs e)
        {
            await SetFields(TotalNumberOfItems - NumberOfRecordsPerPage);
            SetPage(PagedTable.Last(ListForPage, NumberOfRecordsPerPage).DefaultView);
        }

        public async Task Forward_Click(object sender, RoutedEventArgs e)
        {
            await SetFields(NumberOfRecordsPerPage + NextNumber);
            SetPage(PagedTable.Next(ListForPage, NumberOfRecordsPerPage).DefaultView);
        }

        public async Task Backwards_Click(object sender, RoutedEventArgs e)
        {
            await SetFields(NextNumber - NumberOfRecordsPerPage);
            SetPage(PagedTable.Previous(ListForPage, NumberOfRecordsPerPage).DefaultView);
        }

        public async Task First_Click(object sender, RoutedEventArgs e)
        {
            await SetFields(FIRST_NUMBER);
            SetPage(PagedTable.First(ListForPage, NumberOfRecordsPerPage).DefaultView);
        }
        public async Task Refresh(EventFilter filter = EventFilter.NONE, int? categoryId = null, string orderByAttribute = null)
        {
            Filter = filter;
            OrderByAttribute = orderByAttribute;
            TotalNumberOfItems = await GetNumberOfItems(Filter, IdCategory);
            ListForPage = await GetData(FIRST_NUMBER, NumberOfRecordsPerPage, filter, categoryId, orderByAttribute);
            NextNumber = FIRST_NUMBER;
            SetPage(PagedTable.First(ListForPage, NumberOfRecordsPerPage).DefaultView);
        }
        public string PageNumberDisplay()
        {
            int PagedNumber = (NextNumber + NumberOfRecordsPerPage) > TotalNumberOfItems ? TotalNumberOfItems : NextNumber + NumberOfRecordsPerPage;
            return language.ShowingResults + (PagedNumber + @"/" + TotalNumberOfItems);
        }
        protected async Task SetFields(int nextNumber)
        {
            if (nextNumber <= 0)
            {
                nextNumber = 0;
            }
            NextNumber = nextNumber;
            ListForPage = await GetData(NextNumber, NumberOfRecordsPerPage, Filter, IdCategory, OrderByAttribute);
        }
        public void SetPage(DataView dataViewToDisplay)
        {
            DataGrid.ItemsSource = dataViewToDisplay;
            PageInfo.Content = PageNumberDisplay();
        }

        private async Task<IList> GetData(int index, int number, EventFilter filter = EventFilter.NONE, int? categoryId = null, string orderByAttribute = null)
        {
            string cityName = Shared.Config.Properties.Default.City;
            List<Event> categories = await eventService.GetRangeInOneCityWithFilter(index, number, cityName, filter, categoryId, orderByAttribute);
            List<EventViewModel> models = Mapping.Mapper.Map<List<Event>, List<EventViewModel>>(categories);
            return models;
        }

        public void InitializeColumns()
        {
            ResourceManager rm = new ResourceManager("Eve.Shared.Config.language", Assembly.GetExecutingAssembly());
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            foreach (var x in DataGrid.Columns)
            {
                x.Header = rm.GetString(x.SortMemberPath, cultureInfo) ?? x.SortMemberPath;
            }
            HideColumns();
        }
        public void HideColumns()
        {
            foreach (var column in DataGrid.Columns)
            {
                if (!column.SortMemberPath.StartsWith("Name") && !column.SortMemberPath.StartsWith("Address") && !column.SortMemberPath.StartsWith("ScheduledOn"))
                {
                    column.Visibility = Visibility.Hidden;
                }
            }
        }

        public async Task<int> GetNumberOfItems(EventFilter filter = EventFilter.NONE, int? idCategory = null)
        {
            string cityName = Shared.Config.Properties.Default.City;
            return await eventService.GetCountInOneCity(cityName, filter, idCategory);
        }
        public Type GetDataType()
        {
            return typeof(EventViewModel);
        }

        private class Pager
        {
            public int PageIndex { get; set; }

            public Type DataType { get; set; }

            public int TotalNumberToDisplay { get; set; }

            private DataTable PagedList = new DataTable();
            public DataTable Next(IList ListToPage, int RecordsPerPage)
            {
                PageIndex++;
                if (PageIndex >= TotalNumberToDisplay / RecordsPerPage)
                {
                    PageIndex = TotalNumberToDisplay / RecordsPerPage;
                }
                PagedList = PageTable(ListToPage);
                return PagedList;
            }

            public DataTable Previous(IList ListToPage, int RecordsPerPage)
            {
                PageIndex--;
                if (PageIndex <= FIRST_NUMBER)
                {
                    PageIndex = FIRST_NUMBER;
                }
                PagedList = PageTable(ListToPage);
                return PagedList;
            }


            public DataTable First(IList ListToPage, int RecordsPerPage)
            {
                PageIndex = FIRST_NUMBER;
                PagedList = PageTable(ListToPage);
                return PagedList;
            }

            public DataTable Last(IList ListToPage, int RecordsPerPage)
            {
                PageIndex = ListToPage.Count / RecordsPerPage;
                PagedList = PageTable(ListToPage);
                return PagedList;
            }

            public DataTable PageTable(IList SourceList)
            {
                DataTable TableToReturn = new DataTable();

                ResourceManager rm = new ResourceManager("VirtualDoctor.Shared.Config.language", Assembly.GetExecutingAssembly());
                CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
                foreach (var Column in DataType.GetProperties())
                {
                    TableToReturn.Columns.Add(Column.Name, Column.PropertyType);
                }

                foreach (object item in SourceList)
                {
                    DataRow ReturnTableRow = TableToReturn.NewRow();
                    foreach (var Column in DataType.GetProperties())
                    {
                        ReturnTableRow[Column.Name] = Column.GetValue(item);
                    }
                    TableToReturn.Rows.Add(ReturnTableRow);
                }
                return TableToReturn;
            }

        }

    }
}

