﻿using System;
using Csla.Serialization;

namespace LearnLanguages.DataAccess.Exceptions
{
  [Serializable]
  public class GetUserFailedException : Exception
  {
    public GetUserFailedException(string username)
      : base(string.Format(DalResources.ErrorMsgUsernameNotFoundException, username))
    {
      Username = username;
    }

    public GetUserFailedException(string errorMsg, string username)
      : base(errorMsg)
    {
      Username = username;
    }

    public GetUserFailedException(Exception innerException, string username)
      : base(DalResources.ErrorMsgUsernameNotFoundException, innerException)
    {
      Username = username;
    }

    public string Username { get; private set; }
  }
}
