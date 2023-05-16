import React from 'react';
import { Router } from './router';
import {FormProvider} from './contexts/FormContext';

import { GlobalStyles } from './styles/GlobalStyles';

const App = () => {
  return (
    <React.Fragment>
    <GlobalStyles />
    <FormProvider>
      <Router />
      </FormProvider>
      </React.Fragment>
  );
}

export default App;