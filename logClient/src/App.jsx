import { useState, useEffect } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'

import axios from 'axios';

// import './App.css'
import Logs from './Logs';

function App() {
  // const [ac, setAc] = useState([]);

  // useEffect(() => {

  //   axios.get('https://localhost:7269/API/aircraft')
  //   .then((res) => {
  //     // console.log(res.data)
  //     setAc(res.data)
  //   });
  // }, []);

  return (
    <>
    <Logs></Logs>

    {/* {
      ac.map(aircraft => (

        <div key={aircraft.acID}>
          <h1>{aircraft.registration}</h1>
          <p>{aircraft.model}</p>


        </div>

      ))

    } */}

    </>
  )
}

export default App
