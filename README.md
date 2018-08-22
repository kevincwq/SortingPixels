# SortingPixels

Weiqing Chen, 2018/8/22

## Main Points

- WPF MVVM for UI
- Use pure WPF Image, ImageSource, BitmapImage
- Use byte[] as the back buffer for manipulations
- Use bucket sort on each line of image buffer

## Performance Improve

- Time consumption vs Memory consumption 

```csharp

    public BitmapSource SortByHue()
    {
        // process line by line and use less memory
        for (int j = 0; j < Height; j++)
        {
            SortLineByHue(j);
        }

        return CreateBitmapSourceFromBuffer();
    }


    public BitmapSource SortByHueParallel()
    {
        // use parallel for all lines if the time-performance is more important than memory consumption
        Parallel.For(0, Height, j =>
        {
            SortLineByHue(j);
        });

        return CreateBitmapSourceFromBuffer();
    }

```