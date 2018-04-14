
using System;

[Flags]
public enum Shapes
{
    None        = 0,
    All         = Square | Triangle | Circle,
    Square      = 1,
    Triangle    = 1 << 1,
    Circle      = 1 << 2
}