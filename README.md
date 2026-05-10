# Assignment Notes: .NET 8 Development Modules

## 1. Web API Content Evidence
**Module:** Create a web API with ASP.NET Core controllers

**Existing Content (Initial State):**
- ID 1: Classic Italian (Gluten Free: False)
- ID 2: Veggie (Gluten Free: True)

**Updated Content (Including Additional Record):**
Below is the JSON response from `GET /pizza` confirming the third record was successfully added to the internal data store:

```json
[
  { "id": 1, "name": "Classic Italian", "isGlutenFree": false },
  { "id": 2, "name": "Veggie", "isGlutenFree": true },
  { "id": 3, "name": "The .NET Meat-Lover", "isGlutenFree": false }
]


using System.Text;
using System.Collections.Generic;

public static string GenerateSalesSummary(IEnumerable<(string FileName, decimal Total)> fileDetails, decimal aggregateTotal)
{
    StringBuilder sb = new StringBuilder();

    sb.AppendLine("Sales Summary");
    sb.AppendLine("----------------------------");
    sb.AppendLine($" Total Sales: {aggregateTotal:C}");
    sb.AppendLine();
    sb.AppendLine(" Details:");

    foreach (var file in fileDetails)
    {
        sb.AppendLine($"  {file.FileName}: {file.Total:C}");
    }

    return sb.ToString();
}
