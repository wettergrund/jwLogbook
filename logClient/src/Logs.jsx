import 'ka-table/style.css';

import React, { useEffect, useState } from 'react';

import { Table, useTable } from 'ka-table';
import { DataType, EditingMode, SortingMode } from 'ka-table/enums';

import axios from 'axios';


const Logs = () => {
    const [data, setData] = useState([]);
    const table = useTable({

      onDispatch: async () => {

            axios.get('https://localhost:7269/API/logbyuser?id=1')
        .then((res) => {
      console.log(res.data)
      setData(res.data)
      })
        }
    });

    
   

  return (
    <div className='remote-data-demo'>
      <Table
        table={table}
        data={data.slice(0,5)}
        columns= {[
          { key: 'departureAirport', title: 'Dep', dataType: DataType.String },
          { key: 'arrivalAirport', title: 'Arr', dataType: DataType.String },
          { key: 'departureTime', title: 'DepTime', dataType: DataType.Date },

          { key: 'arrivalTime', title: 'ArrTime', dataType: DataType.Date }



        ]}
        sortingMode={SortingMode.Single}
        rowKeyField={'id'}
        format= {({ column, value }) => {
            if (column.key === 'company.hasLoyalProgram'){
              return `Loyal program: ${ value == null ? 'Unspecified' : (value ? 'Yes' : 'No')}`;
            }
            if (column.key === 'price'){
              return value == null ? '-' : `$${value}`;
            }
            if (column.dataType === DataType.Date){
              return value && value.toLocaleDateString('sv', {year: 'numeric', month: '2-digit', day: '2-digit', hour: '2-digit', minute: '2-digit'  });
            }
          }}
      />
    </div>
  )
}

export default Logs