﻿using System;
using Csla.Core;

namespace LearnLanguages.Common.Interfaces
{
  public interface IJobInfo<TTarget> : IJobInfo
  {
    TTarget Target { get; }
  }
}