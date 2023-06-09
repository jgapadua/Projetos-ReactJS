import {useNavigate, Link} from 'react-router-dom'
import * as C from './styles';
import {useForm, FormActions} from '../../contexts/FormContext'
import {Theme} from '../../components/Theme';
import { ChangeEvent, useEffect,useState } from 'react';

let controle : number = 0
export const FormStep3=()=>{
  const navigate = useNavigate();
  const {state, dispatch} = useForm();
  const [confirm, setConfirm] = useState(false)

  useEffect(() => {
    if(confirm == true){
      controle = 4
    }else controle = 3

    if(state.name === ''){
      navigate('/');
    }else{
      dispatch({
        type: FormActions.setCurrentStep,
        payload: controle
    });
    }
  }, [confirm]);

  const handleNextStep = ()=>{
    if(state.email !== '' && state.github !== ''){
      console.log(state);
      setConfirm(true)
    }else{
      alert("Preencha os dados");
    }
  }

    const handleEmailChange = (e:ChangeEvent<HTMLInputElement>)=>{
      dispatch({
        type: FormActions.setEmail,
        payload: e.target.value
      });
    }
    const handleGithubChange = (e:ChangeEvent<HTMLInputElement>)=>{
      dispatch({
        type: FormActions.setGithub,
        payload: e.target.value
      });
    }

  return(
    <Theme>
      <C.Container>
        <p>Passo 3/3</p>
        <h1>Legal {state.name}, onde te achamos?</h1>
        <p>Preencha com seus contatos para conseguirmos entrar em contato.</p>
        <hr/>
        <label>
          Qual seu e-mail?
            <input
                type="email"
                value={state.email}
                onChange={handleEmailChange}
            />
        </label>
        <label>
          Qual seu Github?
            <input
                type="url"
                value={state.github}
                onChange={handleGithubChange}
            />
        </label>
        <Link to="/step2" className="backButton">Voltar</Link>
        <button onClick={handleNextStep}>Finalizar Cadastro</button>
      </C.Container>
    </Theme>
  );
}