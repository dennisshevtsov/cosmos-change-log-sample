// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace CosmosChangeLogSample.Data.Entities
{
  public sealed class FieldChangedRecordEntity
  {
    public string FieldName { get; set; }

    public string PreviousValue { get; set; }

    public string NewValue { get; set; }
  }
}
