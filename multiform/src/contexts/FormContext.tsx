//Context, Reducer, Provider, Hook
import {createContext, useContext, useReducer} from 'react';

//Context
const formContext = createContext(undefined);

//Reducer
enum FormActions{
  setCurrentStep,
  setName,
  setLevel,
  setEmail,
  setGithub
}
const formReducer=(state, action)=>{
    switch(action.type){
      case FormActions.setCurrentStep:
        return {...state, currentStep: action.payload};
      case FormActions.setName:
        return {...state, name:action.payload};
      case FormActions.setLevel:
        return {...state, level:action.payload};
      case FormActions.setEmail:
        return {...state, email:action.payload};
      case FormActions.setGithub:
        return {...state, github:action.payload};
    }
}