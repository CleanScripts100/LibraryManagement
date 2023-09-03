import { Link } from "react-router-dom";
import { ImBooks } from "react-icons/im";
import { SiBookstack } from "react-icons/si";
import { MdOutlineCreateNewFolder } from "react-icons/md";
import {
  Accordion,
  AccordionDetails,
  AccordionGroup,
  AccordionSummary,
  accordionSummaryClasses,
} from "@mui/joy";
import { FaUsers, FaUsersCog, FaAngleRight, FaUserPlus } from "react-icons/fa";

import { useAppSelector } from "../hooks/reduxHooks";

const Sidebar = () => {
  const { user } = useAppSelector((state) => state.user);
  const navsFooter = [
    {
      href: "",
      name: "Help",
      icon: (
        <svg
          xmlns="http://www.w3.org/2000/svg"
          fill="none"
          viewBox="0 0 24 24"
          strokeWidth={1.5}
          stroke="currentColor"
          className="w-5 h-5"
        >
          <path
            strokeLinecap="round"
            strokeLinejoin="round"
            d="M9.879 7.519c1.171-1.025 3.071-1.025 4.242 0 1.172 1.025 1.172 2.687 0 3.712-.203.179-.43.326-.67.442-.745.361-1.45.999-1.45 1.827v.75M21 12a9 9 0 11-18 0 9 9 0 0118 0zm-9 5.25h.008v.008H12v-.008z"
          />
        </svg>
      ),
    },
    {
      href: "",
      name: "Settings",
      icon: (
        <svg
          xmlns="http://www.w3.org/2000/svg"
          fill="none"
          viewBox="0 0 24 24"
          strokeWidth={1.5}
          stroke="currentColor"
          className="w-5 h-5"
        >
          <path
            strokeLinecap="round"
            strokeLinejoin="round"
            d="M9.594 3.94c.09-.542.56-.94 1.11-.94h2.593c.55 0 1.02.398 1.11.94l.213 1.281c.063.374.313.686.645.87.074.04.147.083.22.127.324.196.72.257 1.075.124l1.217-.456a1.125 1.125 0 011.37.49l1.296 2.247a1.125 1.125 0 01-.26 1.431l-1.003.827c-.293.24-.438.613-.431.992a6.759 6.759 0 010 .255c-.007.378.138.75.43.99l1.005.828c.424.35.534.954.26 1.43l-1.298 2.247a1.125 1.125 0 01-1.369.491l-1.217-.456c-.355-.133-.75-.072-1.076.124a6.57 6.57 0 01-.22.128c-.331.183-.581.495-.644.869l-.213 1.28c-.09.543-.56.941-1.11.941h-2.594c-.55 0-1.02-.398-1.11-.94l-.213-1.281c-.062-.374-.312-.686-.644-.87a6.52 6.52 0 01-.22-.127c-.325-.196-.72-.257-1.076-.124l-1.217.456a1.125 1.125 0 01-1.369-.49l-1.297-2.247a1.125 1.125 0 01.26-1.431l1.004-.827c.292-.24.437-.613.43-.992a6.932 6.932 0 010-.255c.007-.378-.138-.75-.43-.99l-1.004-.828a1.125 1.125 0 01-.26-1.43l1.297-2.247a1.125 1.125 0 011.37-.491l1.216.456c.356.133.751.072 1.076-.124.072-.044.146-.087.22-.128.332-.183.582-.495.644-.869l.214-1.281z"
          />
          <path
            strokeLinecap="round"
            strokeLinejoin="round"
            d="M15 12a3 3 0 11-6 0 3 3 0 016 0z"
          />
        </svg>
      ),
    },
    {
      href: "",
      name: "Logout",
      icon: (
        <svg
          xmlns="http://www.w3.org/2000/svg"
          fill="none"
          viewBox="0 0 24 24"
          strokeWidth={1.5}
          stroke="currentColor"
          className="w-5 h-5"
        >
          <path
            strokeLinecap="round"
            strokeLinejoin="round"
            d="M15.75 9V5.25A2.25 2.25 0 0013.5 3h-6a2.25 2.25 0 00-2.25 2.25v13.5A2.25 2.25 0 007.5 21h6a2.25 2.25 0 002.25-2.25V15m3 0l3-3m0 0l-3-3m3 3H9"
          />
        </svg>
      ),
    },
  ];

  return (
    <>
      <nav className="fixed top-0 left-0 w-full h-full border-r bg-white space-y-8 sm:w-60">
        <div className="flex flex-col h-full">
          <div className="h-20 flex items-center px-8">
            <a href="/" className="flex-none text-3xl">
              BooKing
            </a>
          </div>
          <div className="flex-1 flex flex-col h-full overflow-auto">
            <ul className="px-4 text-sm font-medium flex-1">
              <li>
                <Link
                  to="/admin/books"
                  className="flex items-center gap-x-2 text-gray-600 p-2 rounded-lg  hover:bg-gray-50 active:bg-gray-100 duration-150"
                >
                  <div className="text-gray-500">
                    <svg
                      xmlns="http://www.w3.org/2000/svg"
                      fill="none"
                      viewBox="0 0 24 24"
                      strokeWidth={1.5}
                      stroke="currentColor"
                      className="w-5 h-5"
                    >
                      <path
                        strokeLinecap="round"
                        strokeLinejoin="round"
                        d="M6 6.878V6a2.25 2.25 0 012.25-2.25h7.5A2.25 2.25 0 0118 6v.878m-12 0c.235-.083.487-.128.75-.128h10.5c.263 0 .515.045.75.128m-12 0A2.25 2.25 0 004.5 9v.878m13.5-3A2.25 2.25 0 0119.5 9v.878m0 0a2.246 2.246 0 00-.75-.128H5.25c-.263 0-.515.045-.75.128m15 0A2.25 2.25 0 0121 12v6a2.25 2.25 0 01-2.25 2.25H5.25A2.25 2.25 0 013 18v-6c0-.98.626-1.813 1.5-2.122"
                      />
                    </svg>
                  </div>
                  Overview
                </Link>
              </li>
              <li>
                <AccordionGroup
                  sx={{
                    maxWidth: 400,
                    [`& .${accordionSummaryClasses.indicator}`]: {
                      transition: "0.2s",
                    },
                    [`& [aria-expanded="true"] .${accordionSummaryClasses.indicator}`]:
                      {
                        transform: "rotate(90deg)",
                      },
                  }}
                >
                  <Accordion>
                    <AccordionSummary indicator={<FaAngleRight />}>
                      <ImBooks />
                      Books
                    </AccordionSummary>
                    <AccordionDetails>
                      <Link
                        to="/admin/books"
                        className="flex items-center gap-x-2 text-gray-600 p-2 rounded-lg  hover:bg-gray-50 active:bg-gray-100 duration-150"
                      >
                        <div className="text-gray-500">
                          <SiBookstack />
                        </div>
                        All Books
                      </Link>
                      <Link
                        to="/admin/create-book"
                        className="flex items-center gap-x-2 text-gray-600 p-2 rounded-lg  hover:bg-gray-50 active:bg-gray-100 duration-150"
                      >
                        <div className="text-gray-500">
                          <MdOutlineCreateNewFolder />
                        </div>
                        Create Book
                      </Link>
                    </AccordionDetails>
                  </Accordion>
                </AccordionGroup>
              </li>

              <li>
                <AccordionGroup
                  sx={{
                    maxWidth: 400,
                    [`& .${accordionSummaryClasses.indicator}`]: {
                      transition: "0.2s",
                    },
                    [`& [aria-expanded="true"] .${accordionSummaryClasses.indicator}`]:
                      {
                        transform: "rotate(90deg)",
                      },
                  }}
                >
                  <Accordion>
                    <AccordionSummary
                      indicator={<FaAngleRight />}
                      className="justify-start"
                    >
                      <FaUsersCog />
                      Manage Users
                    </AccordionSummary>
                    <AccordionDetails>
                      <Link
                        to="/admin/users"
                        className="flex justify-start items-center gap-x-2 text-gray-600 p-2 rounded-lg  hover:bg-gray-50 active:bg-gray-100 duration-150"
                      >
                        <div>
                          {" "}
                          <FaUsers />
                        </div>
                        All Users
                      </Link>

                      <Link
                        to="/admin/create-admin"
                        className="flex items-center justify-start gap-x-2 text-gray-600 p-2 rounded-lg  hover:bg-gray-50 active:bg-gray-100 duration-150"
                      >
                        <div className="text-gray-500">
                          <FaUserPlus />
                        </div>
                        Create Admin User
                      </Link>
                    </AccordionDetails>
                  </Accordion>
                </AccordionGroup>
              </li>
            </ul>
            <div>
              <ul className="px-4 pb-4 text-sm font-medium">
                {navsFooter.map((item, idx) => (
                  <li key={idx}>
                    <a
                      href={item.href}
                      className="flex items-center gap-x-2 text-gray-600 p-2 rounded-lg  hover:bg-gray-50 active:bg-gray-100 duration-150"
                    >
                      <div className="text-gray-500">{item.icon}</div>
                      {item.name}
                    </a>
                  </li>
                ))}
              </ul>
              <div className="py-4 px-4 border-t">
                <div className="flex items-center gap-x-4">
                  <img src={user?.avatar} className="w-12 h-12 rounded-full" />
                  <div>
                    <span className="block text-gray-700 text-sm font-semibold">
                      {user?.firstName} {user?.lastName}
                    </span>
                    <Link
                      to="/profile"
                      className="block mt-px text-gray-600 hover:text-indigo-600 text-xs"
                    >
                      View profile
                    </Link>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </nav>
    </>
  );
};

export default Sidebar;
