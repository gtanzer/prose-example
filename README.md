build with `dotnet build`

run with `dotnet run`

provide the i/o example: `"f","fff"`

it only synthesizes the program `Append (v, Append(v, v))`, not `Append (Append(v, v), v)`
