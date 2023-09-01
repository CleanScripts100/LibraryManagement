
import Sidebar from '../components/SideNav'
import DashNav from '../components/DashNav';

import { Outlet } from 'react-router-dom';
import { useAppSelector } from '../hooks/reduxHooks';
const Dashboard = () => {
const {books}   = useAppSelector((state)=> state.books)
console.log(books);


  return (
    <div>
      <Sidebar />

      <div className="p-4 sm:ml-64">
        <DashNav />

        <div className='pt-10'>
     <Outlet/>
        </div>
      </div>
    </div>
  );
}

export default Dashboard