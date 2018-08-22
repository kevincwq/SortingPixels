using SortingPixels.Models;
using System;
using System.Diagnostics;
using System.Windows.Input;
using System.Windows.Media;

namespace SortingPixels.ViewModels
{
    class HueSortViewModel : ViewModelBase
    {
        ICommand randomize, sort;
        ImageSource image;
        HueSortModel model;
        bool isBusy;

        public HueSortViewModel()
        {
            Image = null;
            isBusy = false;
        }

        public double RenderWidth { get; set; }
        public double RenderHeight { get; set; }

        public ImageSource Image { get => image; private set => SetProperty(ref image, value); }

        public ICommand Randomize
        {
            get
            {
                return randomize ?? (randomize = new RelayCommand(o =>
                {
                    try
                    {
                        isBusy = true;
                        Debug.WriteLine($"{RenderWidth} {RenderHeight}");
                        if (model == null || model.Width != (int)RenderWidth || model.Height != (int)RenderHeight)
                        {
                            model = new HueSortModel((int)RenderWidth, (int)RenderHeight);
                        }
                        Image = model.Randomize();
                        GC.Collect();
                    }
                    finally
                    {
                        isBusy = false;
                    }
                }, o => !isBusy));
            }
        }

        public ICommand Sort
        {
            get
            {
                return sort ?? (sort = new RelayCommand(o =>
                {
                    try
                    {
                        isBusy = true;
                        Debug.WriteLine($"{RenderWidth} {RenderHeight}");
                        Image = model.SortByHueParallel();
                        GC.Collect();
                    }
                    finally
                    {
                        isBusy = false;
                    }
                }, o => !isBusy && model != null));
            }
        }
    }
}
