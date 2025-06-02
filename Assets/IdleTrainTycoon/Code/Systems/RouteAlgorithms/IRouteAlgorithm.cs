using System.Collections.Generic;

namespace IdleTrainTycoon.Code.Systems.RouteAlgorithms
{
    public interface IRouteAlgorithm
    {
        List<RouteData> GetPossible(RouteRequest data);
        RouteData GetShortest(RouteRequest data);

    }
}