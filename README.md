build with `dotnet build`

run with `dotnet run`

provide the i/o example: `"f","fff"`

it only synthesizes the program `Append (v, Append(v, v))`, not `Append (Append(v, v), v)`

on branch `rev-cond` (where conditioning is reversed), it synthesizes `Append (Append(v, v), v)`

on branch `disjunctified`, something is wrong and it enters an infinite loop of the first witness function (the conditioned one never gets invoked)
