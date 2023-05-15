import * as C from './App.styles';
import {Character} from './components/Character';

const App =()=>{
  return(
    <C.Container>
      <C.Map>
      <Character x={30} y={30} />
      </C.Map>
    </C.Container>
  );
}
export default App;