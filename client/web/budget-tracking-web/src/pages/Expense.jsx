import React from 'react'
import ProcessButton from '../components/Buttons/ProcessButton'

const Expense = () => {
  return (
    <>
      <div id="expense-container" style={{backgroundColor:"#F2F2F2"}} className='w-full'>



      
        <div id="process-btn-group" className='absolute bottom-10 right-10'>{/* sağ ve aşağı hizalama */}
          <ProcessButton padding={"p-5"} margin={"mb-3"} backgroundColor={"bg-[#4CAF50]"} type={"add"}/>
          <ProcessButton padding={"p-5"} backgroundColor={"bg-[#F44336]"} type={"subtract"} />
        </div>
      </div>
    </>
  )
}

export default Expense
