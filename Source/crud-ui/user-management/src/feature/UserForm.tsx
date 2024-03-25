import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { z } from "zod";
import { Button } from "@/components/ui/button";

import {
  Form,
  FormControl,
  FormDescription,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form";
import { Input } from "@/components/ui/input";
import { User } from "@/model/User";
import { createUser, updateUser } from "@/services/userService";
import { useEffect } from "react";

const formSchema = z.object({
  LocationId: z.number(),
  EmployeeType: z.string().nonempty({ message: "Employee Type is required" }),
  Name: z.string().min(2, { message: "Name must be at least 2 characters" }),
  MobileNo: z
    .string()
    .nonempty({ message: "Mobile No is required" })
    .regex(/^\d{0,10}$/, {
      message: "Mobile Number must contain up to 10 numbers",
    }),
  Email: z.string().email({ message: "Invalid email address" }),
  Nationality: z.string().nonempty({ message: "Nationality is required" }),
  Designation: z.string().nonempty({ message: "Designation is required" }),
  PassportNo: z.string().nonempty({ message: "Passport Number is required" }),
  PassportExpiryDate: z.date().refine((date) => date > new Date(), {
    message: "Passport expiry date must be in the future",
  }),
  PassportFile: z
    .any()
    .refine((file) => file?.length > 1, "Passport is required"),
  PersonPhoto: z.any().refine((file) => file?.length > 1, "Photo is required"),
});

const defaultValues: User = {
  LocationId: 0,
  EmployeeType: "",
  Name: "",
  MobileNo: "",
  Email: "",
  Nationality: "",
  Designation: "",
  PassportNo: "",
  PassportExpiryDate: new Date(),
  PassportFile: undefined,
  PersonPhoto: undefined,
};

export function UserForm({
  row,
  isEdit,
}: {
  row?: User;
  isEdit?: (clearData: any) => void;
}) {
  const form = useForm<z.infer<typeof formSchema>>({
    resolver: zodResolver(formSchema),
    defaultValues: row === undefined ? defaultValues : row,
  });

  useEffect(() => {
    console.log(form.getValues());
  }, [form.formState]);

  function onSubmit(values: z.infer<typeof formSchema>) {
    return new Promise(async (resolve, reject) => {
      try {
        if (row) {
          const createdUserId = await updateUser(values.LocationId, values);
          console.log("User updated successfully with ID:", createdUserId);
          alert(`User data is updated`);
        } else {
          const createdUserId = await createUser(values);
          console.log("User created successfully with ID:", createdUserId);
          alert(`User data is created`);
        }
      } catch (error) {
        console.error("Error creating user:", error);
      }
    });
  }

  const handleClearFormData = () => {
    isEdit && isEdit(null);
    // const fieldNames = Object.keys(form.register);

    // fieldNames.forEach((fieldName: any) => {
    //   if (fieldName !== "PassportExpiryDate") {
    //     form.setValue(fieldName, "");
    //   }
    // });
    form.reset(defaultValues);
  };

  return (
    <Form {...form}>
      <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-8">
        <FormField
          control={form.control}
          name="Name"
          render={({ field }) => (
            <FormItem>
              <FormLabel className="block text-left">Name</FormLabel>
              <FormControl>
                <Input placeholder="Enter Name" {...field} />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        <div className="flex flex-wrap -mx-4">
          <div className="w-1/3 md:w-1/3 px-4 mb-4">
            <FormField
              control={form.control}
              name="EmployeeType"
              render={({ field }) => (
                <FormItem>
                  <FormLabel className="block text-left">
                    Employee Type
                  </FormLabel>
                  <FormControl>
                    <Input placeholder="Enter Employee Type" {...field} />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
          </div>
          <div className="w-full md:w-1/2 px-4 mb-4">
            <FormField
              control={form.control}
              name="MobileNo"
              render={({ field }) => (
                <FormItem>
                  <FormLabel className="block text-left">
                    Mobile Number
                  </FormLabel>
                  <FormControl>
                    <Input
                      placeholder="Enter Mobile Number"
                      pattern="[0-9]*"
                      type="number"
                      {...field}
                    />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
          </div>
        </div>
        <div className="flex flex-wrap -mx-4">
          <div className="w-full md:w-1/2 px-4 mb-4">
            <FormField
              control={form.control}
              name="Email"
              render={({ field }) => (
                <FormItem>
                  <FormLabel className="block text-left">Email</FormLabel>
                  <FormControl>
                    <Input placeholder="Enter Email" type="email" {...field} />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
          </div>
          <div className="w-full md:w-1/2 px-4 mb-4">
            <FormField
              control={form.control}
              name="Nationality"
              render={({ field }) => (
                <FormItem>
                  <FormLabel className="block text-left">Nationality</FormLabel>
                  <FormControl>
                    <Input placeholder="Enter Nationality" {...field} />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
          </div>
        </div>
        <div className="flex flex-wrap -mx-4">
          <div className="w-full md:w-1/2 px-4 mb-4">
            <FormField
              control={form.control}
              name="PassportNo"
              render={({ field }) => (
                <FormItem>
                  <FormLabel className="block text-left">
                    Passport Number
                  </FormLabel>
                  <FormControl>
                    <Input placeholder="Enter Passport Number" {...field} />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
          </div>
          <div className="w-1/2 md:w-1/3 px-4 mb-4">
            <FormField
              control={form.control}
              name="PassportExpiryDate"
              render={({ field }) => (
                <FormItem>
                  <FormLabel className="block text-left">
                    Passport Expiry Date
                  </FormLabel>
                  <FormControl>
                    <Input
                      type="date"
                      {...field}
                      value={
                        field.value
                          ? new Date(field.value).toISOString().split("T")[0]
                          : new Date().toISOString().split("T")[0]
                      }
                      onChange={(e) => field.onChange(new Date(e.target.value))}
                    />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
          </div>
        </div>

        <FormField
          control={form.control}
          name="Designation"
          render={({ field }) => (
            <FormItem>
              <FormLabel className="block text-left">Designation</FormLabel>
              <FormControl>
                <Input placeholder="Enter Designation" {...field} />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        {!row && (
          <FormField
            control={form.control}
            name="PassportFile"
            render={({ field }) => (
              <FormItem>
                <FormLabel className="block text-left">Passport</FormLabel>
                <FormControl>
                  <Input type="file" accept=".pdf,.jpg,.png" {...field} />
                </FormControl>
                <FormMessage />
              </FormItem>
            )}
          />
        )}
        {!row && (
          <FormField
            control={form.control}
            name="PersonPhoto"
            render={({ field }) => (
              <FormItem>
                <FormLabel className="block text-left">Photo</FormLabel>
                <FormControl>
                  <Input type="file" accept=".jpg,.png" {...field} />
                </FormControl>
                <FormMessage />
              </FormItem>
            )}
          />
        )}
        {row && (
          <Button className="w-1/3 mr-2" onClick={handleClearFormData}>
            Clear
          </Button>
        )}
        <Button className="w-1/3 ml-2" type="submit">
          Submit
        </Button>
      </form>
    </Form>
  );
}
