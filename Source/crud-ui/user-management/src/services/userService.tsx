import axios, { AxiosResponse } from "axios";
import { User } from "@/model/User";

const BASE_URL = "http://localhost:5194/api/user";

export async function getUsers(): Promise<any> {
  try {
    const response: AxiosResponse<any> = await axios.get(BASE_URL);
    return response.data;
  } catch (error) {
    console.error("Error fetching users:", error);
    throw error;
  }
}

export async function createUser(userData: Partial<User>): Promise<number> {
  try {
    const formData = new FormData();
    Object.entries(userData).forEach(([key, value]) => {
      if (key === "PassportFile" || key === "PersonPhoto") {
        const fileInput = document.querySelector(
          `input[name="${key}"]`
        ) as HTMLInputElement;
        if (fileInput && fileInput.files && fileInput.files.length > 0) {
          const file = fileInput.files[0];
          formData.append(key, file);
        }
      } else {
        if (typeof value === "string" || typeof value === "number") {
          formData.append(key, String(value));
        } else if (value instanceof Date) {
          formData.append(key, value.toISOString());
        } else {
          formData.append(key, value);
        }
      }
    });

    const response: AxiosResponse<number> = await axios.post(
      BASE_URL,
      formData,
      {
        headers: {
          "Content-Type": "multipart/form-data",
        },
      }
    );
    return response.data;
  } catch (error) {
    console.error("Error creating user:", error);
    throw error;
  }
}
async function getUserById(userId: number): Promise<User> {
  try {
    const response: AxiosResponse<User> = await axios.get(
      `${BASE_URL}/${userId}`
    );
    return response.data;
  } catch (error) {
    console.error(`Error fetching user with ID ${userId}:`, error);
    throw error;
  }
}

export async function updateUser(
  userId: number,
  updatedUserData: Partial<User>
): Promise<number> {
  try {
    const formData = new FormData();
    Object.entries(updatedUserData).forEach(([key, value]) => {
      if (key === "PassportFile" || key === "PersonPhoto") {
        const fileInput = document.querySelector(
          `input[name="${key}"]`
        ) as HTMLInputElement;
        if (fileInput && fileInput.files && fileInput.files.length > 0) {
          const file = fileInput.files[0];
          formData.append(key, file);
        }
      } else {
        if (typeof value === "string" || typeof value === "number") {
          formData.append(key, String(value));
        } else if (value instanceof Date) {
          formData.append(key, value.toISOString());
        } else {
          formData.append(key, value);
        }
      }
    });

    const response: AxiosResponse<number> = await axios.put(
      `${BASE_URL}/${userId}`,
      formData
    );
    return response.data;
  } catch (error) {
    console.error(`Error updating user with ID ${userId}:`, error);
    throw error;
  }
}

export async function deleteUser(userId: number): Promise<void> {
  try {
    await axios.delete(`${BASE_URL}/${userId}`);
    console.log("User deleted successfully");
  } catch (error) {
    console.error(`Error deleting user with ID ${userId}:`, error);
    throw error;
  }
}
