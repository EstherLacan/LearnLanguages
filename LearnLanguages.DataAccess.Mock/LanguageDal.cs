﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LearnLanguages.DataAccess.Mock
{
  public class LanguageDal : ILanguageDal
  {
    public Result<LanguageDto> New(object criteria)
    {
      Result<LanguageDto> retResult = Result<LanguageDto>.Undefined(null);
      try
      {
        var dto = new LanguageDto() { Id = Guid.NewGuid() };
        retResult = Result<LanguageDto>.Success(dto);
      }
      catch (Exception ex)
      {
        retResult = Result<LanguageDto>.FailureWithInfo(null, ex);
      }
      return retResult;
    }
    public Result<LanguageDto> Fetch(Guid id)
    {
      Result<LanguageDto> retResult = Result<LanguageDto>.Undefined(null);
      try
      {
        var results = from item in SeedData.Instance.Languages
                      where item.Id == id
                      select item;

        if (results.Count() == 1)
          retResult = Result<LanguageDto>.Success(results.First());
        else
        {
          if (results.Count() == 0)
            retResult = Result<LanguageDto>.FailureWithInfo(null,
              new Exceptions.FetchFailedException(DalResources.ErrorMsgIdNotFoundException));
          else
            retResult = Result<LanguageDto>.FailureWithInfo(null, new Exceptions.FetchFailedException());
        }
      }
      catch (Exception ex)
      {
        retResult = Result<LanguageDto>.FailureWithInfo(null, ex);
      }
      return retResult;
    }
    public Result<LanguageDto> Update(LanguageDto dto)
    {
      Result<LanguageDto> retResult = Result<LanguageDto>.Undefined(null);
      try
      {
        var results = from item in SeedData.Instance.Languages
                      where item.Id == dto.Id
                      select item;

        if (results.Count() == 1)
        {
          var languageToUpdate = results.First();
          SeedData.Instance.Languages.Remove(languageToUpdate);
          dto.Id = Guid.NewGuid();
          SeedData.Instance.Languages.Add(dto);
          //UPDATE PHRASES WHO REFERENCE THIS LANGUAGE
          UpdateReferences(languageToUpdate, dto);

          retResult = Result<LanguageDto>.Success(dto);
        }
        else
        {
          if (results.Count() == 0)
            retResult = Result<LanguageDto>.FailureWithInfo(null,
              new Exceptions.UpdateFailedException(DalResources.ErrorMsgIdNotFoundException));
          else
            retResult = Result<LanguageDto>.FailureWithInfo(null, new Exceptions.FetchFailedException());
        }
      }
      catch (Exception ex)
      {
        retResult = Result<LanguageDto>.FailureWithInfo(null, ex);
      }
      return retResult;
    }
    public Result<LanguageDto> Insert(LanguageDto dto)
    {
      Result<LanguageDto> retResult = Result<LanguageDto>.Undefined(null);
      try
      {
        var results = from item in SeedData.Instance.Languages
                      where item.Id == dto.Id
                      select item;

        if (results.Count() == 0)
        {
          dto.Id = Guid.NewGuid();
          SeedData.Instance.Languages.Add(dto);
          retResult = Result<LanguageDto>.Success(dto);
        }
        else
        {
          if (results.Count() == 1) //ID ALREADY EXISTS
            retResult = Result<LanguageDto>.FailureWithInfo(dto,
              new Exceptions.UpdateFailedException(DalResources.ErrorMsgIdNotFoundException));
          else                      //MULTIPLE IDS ALREADY EXIST??
            retResult = Result<LanguageDto>.FailureWithInfo(null, new Exceptions.FetchFailedException());
        }
      }
      catch (Exception ex)
      {
        retResult = Result<LanguageDto>.FailureWithInfo(null, ex);
      }
      return retResult;
    }
    public Result<LanguageDto> Delete(Guid id)
    {
      Result<LanguageDto> retResult = Result<LanguageDto>.Undefined(null);
      try
      {
        var results = from item in SeedData.Instance.Languages
                      where item.Id == id
                      select item;

        if (results.Count() == 1)
        {
          var languageToRemove = results.First();
          SeedData.Instance.Languages.Remove(languageToRemove);
          retResult = Result<LanguageDto>.Success(languageToRemove);
        }
        else
        {
          if (results.Count() == 0)
            retResult = Result<LanguageDto>.FailureWithInfo(null,
              new Exceptions.DeleteFailedException(DalResources.ErrorMsgIdNotFoundException));
          else
            retResult = Result<LanguageDto>.FailureWithInfo(null, new Exceptions.DeleteFailedException());
        }
      }
      catch (Exception ex)
      {
        retResult = Result<LanguageDto>.FailureWithInfo(null, ex);
      }
      return retResult;
    }
    public LearnLanguages.Result<ICollection<LanguageDto>> GetAll()
    {
      Result<ICollection<LanguageDto>> retResult = Result<ICollection<LanguageDto>>.Undefined(null);
      try
      {
        var allDtos = new List<LanguageDto>(SeedData.Instance.Languages);
        retResult = Result<ICollection<LanguageDto>>.Success(allDtos);
      }
      catch (Exception ex)
      {
        retResult = Result<ICollection<LanguageDto>>.FailureWithInfo(null, ex);
      }
      return retResult;
    }

    private void UpdateReferences(LanguageDto oldLanguageDto, LanguageDto newLanguageDto)
    {
      //UPDATE PHRASES
      var referencedPhrases = from p in SeedData.Instance.Phrases
                              where p.LanguageId == oldLanguageDto.Id
                              select p;

      foreach (var phrase in referencedPhrases)
      {
        phrase.LanguageId = newLanguageDto.Id;
      }
    }
  }
}
