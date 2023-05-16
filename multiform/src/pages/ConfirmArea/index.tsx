import * as C from './styles'
import { useForm } from '../../contexts/FormContext';

export const ConfirmArea = () =>{
        const { state } = useForm();
        console.log(state.currentStep)
    return(
        <>
        {state.currentStep === 4 ?
        <C.Container>
        <hr/>
        <p>Confirmação de dados</p>
        <label>Seu nome:</label>
        <h1>{state.name}</h1>
        <label>Seu E-mail:</label>
        <h1>{state.email}</h1>
        <label>Seu GitHub:</label>
        <h1>{state.github}</h1>
        <label>Seu nível de experiência:</label>
        <h1>{state.level ? 'Experiente' : 'Iniciante'}</h1>

        </C.Container>
        : <div></div>}
        </>
    )}