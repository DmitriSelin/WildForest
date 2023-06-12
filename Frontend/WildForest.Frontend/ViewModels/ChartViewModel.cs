using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using System.Collections.Generic;
using System.Linq;
using WildForest.Frontend.Contracts.Weather.Models;
using WildForest.Frontend.ViewModels.ChartModels;

namespace WildForest.Frontend.ViewModels;

internal partial class ChartViewModel : ObservableObject
{
    public ISeries[] Series { get; set; } = null!;

	internal void FillCollections(List<WeatherForecastDto> forecasts)
	{
        Series = new ISeries[]
        {
            new LineSeries<ChartDto>
            {
                TooltipLabelFormatter = point => $"{point.Model?.Time}",
                Values = forecasts.Select(x => new ChartDto(x.Time, x.Temperature.Value)),
                Mapping = (dto, point) =>
                {
                    point.PrimaryValue = dto.Temperature;
                    point.SecondaryValue = point.Index;
                }
            }
        };
    }
}