﻿using System;
using LearnLanguages.Common.Interfaces;
using Caliburn.Micro;

namespace LearnLanguages.Offer
{
  public class Exchange : IExchange
  {
    public Exchange()
    {
      _EventAggregator = Services.EventAggregator;
      if (_EventAggregator == null)
        throw new Exception();
    }

    #region Singleton Pattern Members
    private static volatile Exchange _Ton;
    private static object _Lock = new object();
    public static Exchange Ton
    {
      get
      {
        if (_Ton == null)
        {
          lock (_Lock)
          {
            if (_Ton == null)
              _Ton = new Exchange();
          }
        }

        return _Ton;
      }
    }
    #endregion

    #region private static EventAggregator
    private static object eventAggregatorLock = new object();
    private static volatile IEventAggregator _eventAggregator;
    private static IEventAggregator _EventAggregator 
    {
      get
      {
        lock (eventAggregatorLock)
        {
          return _eventAggregator;
        }
      }
      set
      {
        lock (eventAggregatorLock)
        {
          _eventAggregator = value;
        }
      }
    }
    #endregion

    public Guid Id
    {
      get { return Guid.Parse(OfferResources.OfferExchangeId); }
    }

    public void Publish(IOpportunity opportunity)
    {
      throw new NotImplementedException();
    }
    public void Publish(IOffer offer)
    {
      throw new NotImplementedException();
    }
    public void Publish(IOfferResponse offerResponse)
    {
      throw new NotImplementedException();
    }
    public void Publish(IWorkStatusUpdate workStatusUpdate)
    {
      throw new NotImplementedException();
    }

    public void SubscribeToOpportunities(object subscriber)
    {
      _EventAggregator.Subscribe(subscriber);
    }
    public void UnsubscribeFromOpportunities(object subscriber)
    {
      _EventAggregator.Unsubscribe(subscriber);
    }

    public void SubscribeToOffers(object subscriber)
    {
      _EventAggregator.Subscribe(subscriber);
    }
    public void UnsubscribeFromOffers(object subscriber)
    {
      _EventAggregator.Unsubscribe(subscriber);
    }

    public void SubscribeToOfferResponses(object subscriber)
    {
      _EventAggregator.Subscribe(subscriber);
    }
    public void UnsubscribeFromOfferResponses(object subscriber)
    {
      _EventAggregator.Unsubscribe(subscriber);
    }

    public void SubscribeToWorkStatusUpdates(object subscriber)
    {
      _EventAggregator.Subscribe(subscriber);
    }
    public void UnsubscribeFromWorkStatusUpdates(object subscriber)
    {
      _EventAggregator.Unsubscribe(subscriber);
    }
  }
}
