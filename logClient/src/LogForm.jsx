import React, { useEffect, useState } from 'react'
import DatePicker from 'react-datepicker'
import "react-datepicker/dist/react-datepicker.css";

import axios from 'axios';

// - aircraft
// - dep
// - arr
// - block off
// - block on

const LogForm = () => {

  const [ac, setAc] = useState([]);

  const [aircraft, setAircraft] = useState(0)
  const [dep, setDep] = useState("From")
  const [arr, setArr] = useState("To")
  const [start, setStart] = useState(new Date())
  const [end, setEnd] = useState(new Date())

  const [diff, setDiff] = useState(0);
  

  const options = { year: 'numeric', month: '2-digit', day: '2-digit', hour: '2-digit', minute: '2-digit' }

  const getDiff = (from, to) =>{
    const diffInMilliSeconds = to - from;
    const diffInSeconds = diffInMilliSeconds / 1000;
    const diffInHours = diffInSeconds / (60 * 60);

    return diffInHours;
  }

  useEffect(() => {
    setDiff(getDiff(start, end));
    
  }, [end]);
  

  useEffect(() => {
    axios.get('https://localhost:7269/API/aircraft')
    .then((res) => {
    console.log(res.data)
    setAc(res.data)

  
  })
    
  }, []);

  return (

    <>

    
    <form>

        <label>Departure</label><br/>
        <input type='text' onChange={e => (setDep(e.target.value))}></input><br/>
        <label>Time</label>
        <DatePicker
          selected={start}
          onChange={(date) => setStart(date)}
          showTimeSelect
          timeFormat="HH:mm"
          timeIntervals={5}
          timeCaption="time"
          dateFormat="yyyy-MM-dd HH:mm"
        /><br/>
        <label>Arrival</label><br/>
        <input type='text' onChange={e => (setArr(e.target.value))}></input><br/>
        <label>Time</label>
        <DatePicker
          selected={end}
          onChange={(date) => setEnd(date)}
          showTimeSelect
          timeFormat="HH:mm"
          timeIntervals={5}
          timeCaption="time"
          dateFormat="yyyy-MM-dd HH:mm"
        /><br/>
        <select onChange={e => setAircraft(e.target.value)}>
          {
            ac.map((option) => (
              <option key={option.acID} value={option.acID}>
                {option.registration}
              </option>

            )
            )

          }
        </select>

     
  

            

  


        <p>{dep} {start.toLocaleString('sv', options)}</p>
        <p>{arr} {end.toLocaleString('sv', options)}</p>
        <p>{diff}</p>
        <p>{aircraft}</p>

 
    </form>

    </>

    
  )
}

export default LogForm