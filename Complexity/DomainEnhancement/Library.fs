namespace DomainEnhancement

open System

type Id = Id of Guid

type SimpleId = Guid


type ContentString = | HtmlString of string 
                     | MarkdownString of string

type Task = {
        Question: ContentString
        PossibleAnswers: ContentString array
        Hints: ContentString array
        Answer: ContentString
        ParentGeneratorId: Guid
        }
