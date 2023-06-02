import 'ka-table/style.css';

import React, { useEffect, useState } from 'react';

import { Table, useTable } from 'ka-table';
import { DataType, EditingMode, SortingMode, SortDirection, PagingPosition, ActionType  } from 'ka-table/enums';

import axios from 'axios';




const Logs = () => {
    const today = new Date(); // Get the current date
    const [pageIndex, setPageIndex] = useState(0);
    const [data, setData] = useState([]);

    useEffect(() => {
      axios.get('https://localhost:7269/API/logbyuser?id=1&number=20')
      .then((res) => {
      console.log(res.data)
      setData(res.data)

    
    })
      
    }, []);

    const table = useTable({

      
    
      onDispatch: async () => {
        if (action.type === ActionType.UpdatePageIndex) {
          setPageIndex(action.pageIndex);
        }

            
 
        }
    });


    const returnDiff = (flight) =>{

      {console.log(flight)}
      const flightDate = new Date(flight.departureTime); // Convert flight date string to Date object
      {console.log(flightDate)}
      const differenceInTime = flightDate.getTime() - today.getTime(); // Calculate the time difference in milliseconds
      {console.log(differenceInTime)}
      const differenceInDays = differenceInTime / (1000 * 3600 * 24); // Convert milliseconds to days
      {console.log(differenceInDays)}

      return differenceInDays >= 0 && differenceInDays <= 90; // Return true if flight date is within 90 days



    };


    const filteredFlights = data.filter((flight) => {
      const flightDate = new Date(flight.departureTime); // Convert flight date string to Date object
      const differenceInTime = flightDate.getTime() - today.getTime(); // Calculate the time difference in milliseconds
      const differenceInDays = differenceInTime / (1000 * 3600 * 24); // Convert milliseconds to days
  
      return differenceInDays >= 0 && differenceInDays <= 90; // Return true if flight date is within 90 days
    });
  
    



   

  return (
    <div className='remote-data-demo'>
      <Table
        table={table}
        data={data}
        columns= {[
         
          { key: 'departureAirport', title: 'Dep', dataType: DataType.String },
          { key: 'arrivalAirport', title: 'Arr', dataType: DataType.String },
          { key: 'departureTime', title: 'DepTime', dataType: DataType.Date, sortDirection: SortDirection.Descend, },

          { key: 'arrivalTime', title: 'ArrTime', dataType: DataType.Date },
          { key: 'landings', title: 'Landings', dataType: DataType.int },
          { key: 'duration', title: 'Block time', dataType: DataType.decimal }




        ]}
        sortingMode={SortingMode.Single}
        rowKeyField={'flightID'}
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
          editingMode={EditingMode.Cell}
          paging= {{
            enabled: true,
            pageIndex,
            pageSize: 10,
            pageSizes: [10, 50, 100],
            position: PagingPosition.Bottom
          }}
          childComponents={{
            summaryCell: {
              content: ({ column }) => {
                switch (column.key){
                  case 'duration': return (
                    <>
                      <b>Sum: {data.reduce((acc, o) => acc + o.duration, 0)}</b>
                    </>
                  );
                  case 'landings': return (
                    <>
                      <b>Total: {data.reduce((acc, o) => acc + o.landings, 0)} </b>
                      {/* <b>Total: {
                      
                      
                      data.filter((o) => returnDiff(o)).length
                      
                      
                      } </b> */}

                    </>
                  );
                }
              }
            }
          }}
          
          
      />

    </div>
  )
}

export default Logs