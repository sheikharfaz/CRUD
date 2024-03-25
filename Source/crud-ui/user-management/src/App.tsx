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
import { Button } from "./components/ui/button";

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
          className="w-full"
          value={activeTab}
          onValueChange={(data) => {
            setActiveTab(data);
          }}
        >
          <TabsList className="grid w-full grid-cols-2 ">
            <TabsTrigger value="create">
              {userData ? "Update User" : "Create User"}
            </TabsTrigger>
            <TabsTrigger value="view">View User details</TabsTrigger>
          </TabsList>
          <TabsContent
            value="create"
            className="flex justify-center items-center"
          >
            <Card className="w-[800px] block text-center">
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
          </TabsContent>
          <TabsContent
            value="view"
            className="flex justify-center items-center"
          >
            <Card className="w-full">
              <CardHeader>
                <CardTitle>User Details</CardTitle>
              </CardHeader>
              <CardContent>
                <UserDetails handleData={handleEdit}></UserDetails>
              </CardContent>
            </Card>
          </TabsContent>
        </Tabs>
      </div>
    </>
  );
}

export default App;
