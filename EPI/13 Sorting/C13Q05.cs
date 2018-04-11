using System;
using System.Collections.Generic;
using Xunit;
using static EPI.C13_Sorting.Q05;

namespace EPI.C13_Sorting
{
    public class Q05
    {
        public static int FindMaxConcurrentEvents(Event[] events)
        {
            List<Point> points = new List<Point>();

            foreach (Event e in events)
            {
                points.Add(new Start(e.StartTime));
                points.Add(new End(e.EndTime));
            }
            points.Sort((x, y) => {
                if (x.Time > y.Time)
                    return 1;
                if (x.Time < y.Time)
                    return -1;
                // x and y have the same Time, but Start events "come first", which means an overlapping pair of Start and End events will require two rooms
                if (x is Start)
                    return -1;
                return 1;
            });

            return -1;
        }

        private int EventComparer(Point x, Point y)
        {
            return -1;
        }

        private abstract class Point
        {
            public int Time { get; }
            public Point(int time)
            {
                Time = time;
            }
        }
        private class Start : Point
        {
            public Start(int time) : base(time) { }
        }
        private class End : Point
        {
            public End(int time) : base(time) { }
        }

        public struct Event
        {
            public Event(int start, int end, string name = null)
            {
                StartTime = start;
                EndTime = end;
                Name = name;
            }
            public int StartTime { get; }
            public int EndTime { get; }
            public string Name { get; set; }
        }
    }

    public class C13Q05_Tests
    {
        [Fact]
        public void Example()
        {
            Event[] input = new Event[] {
                new Event(1,5),
                new Event(6,10),
                new Event(11,13),
                new Event(14,15),
                new Event(2,7),
                new Event(8,9),
                new Event(12,15),
                new Event(4,5),
                new Event(9,17),
            };
            Assert.Equal(3, Q05.FindMaxConcurrentEvents(input));
        }
    }
}
