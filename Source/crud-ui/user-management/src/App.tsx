import { useState } from "react";
import "./App.css";
import { UserDetails } from "./feature/UserDetails";
import { UserForm } from "./feature/UserForm";
import {
  Card,
  CardContent,
  CardDescription,
  CardFooter,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import { Tabs, TabsContent, TabsList, TabsTrigger } from "@/components/ui/tabs";
import { BackgroundGradient } from "./components/ui/background-gradient";

function App() {
  const [activeTab, setActiveTab] = useState("create");
  const [userData, setUserData] = useState();

  const handleEdit = (activeTab: string, userData: any) => {
    setUserData(userData);
    setActiveTab(activeTab);
    console.log(userData);
  };

  const handleClearFormData = (clearData: any) => {
    setUserData(clearData);
  };

  return (
    <>
      <div className="flex justify-center items-center">
        <Tabs
          defaultValue="create"
          className="w-full py-4"
          value={activeTab}
          onValueChange={(data) => {
            setActiveTab(data);
          }}
        >
          <TabsList className="grid w-full grid-cols-2 bg-black">
            <TabsTrigger value="create">
              {userData ? "Update User" : "Create User"}
            </TabsTrigger>
            <TabsTrigger value="view">View User details</TabsTrigger>
          </TabsList>
          <TabsContent
            value="create"
            className="flex justify-center items-center"
          >
            <BackgroundGradient className="w-[870px] rounded-[22px]  p-4 sm:p-10 bg-white dark:bg-zinc-900">
              <Card className="w-[800px] rounded-[22px] block text-center">
                <CardHeader>
                  <CardTitle>{userData ? "Update" : "Register"}</CardTitle>
                  <CardDescription>
                    Please fill the below form to{" "}
                    {userData ? "Update" : "Register"} a user
                  </CardDescription>
                </CardHeader>
                <CardContent>
                  <UserForm
                    row={userData}
                    isEdit={handleClearFormData}
                  ></UserForm>
                </CardContent>
              </Card>
            </BackgroundGradient>
          </TabsContent>
          <TabsContent
            value="view"
            className="flex justify-center items-center"
          >
            <BackgroundGradient className="w-[1270px] rounded-[22px] p-4 sm:p-10 bg-white dark:bg-zinc-900">
              <Card className="w-[1200px] rounded-[22px] block text-center">
                <CardHeader>
                  <CardTitle>User Details</CardTitle>
                </CardHeader>
                <CardContent>
                  <UserDetails handleData={handleEdit}></UserDetails>
                </CardContent>
              </Card>
            </BackgroundGradient>
          </TabsContent>
        </Tabs>
      </div>
    </>
  );
}

export default App;
