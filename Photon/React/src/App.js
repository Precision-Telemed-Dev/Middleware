import logo from './logo.svg';
import './App.css';
import("@photonhealth/elements");

function App() {  
  return (
    <div className="App">
	  <photon-client 
		  id="abc"
		  org="org_xyz"
		>
		  <photon-prescribe-workflow
			  patient-id="pat_xxx"
			  enable-order="true"
			  pharmacy-id="phr_xxx"
			/>
	  </photon-client>	  
    </div>
  );
}

export default App;
