import axios from "axios";
import { toast } from "react-toastify";

export const handleError = (error: unknown) => {
    if (axios.isAxiosError(error)) {
        if (error.response) {
            const data = error.response.data;
            
            // Handle validation errors
            if (data.errors && Array.isArray(data.errors)) {
                data.errors.forEach((error: string) => {
                    toast.error(error);
                });
                return;
            }

            // Handle single error message
            if (data.error) {
                toast.error(data.error);
                return;
            }

            // Handle array of error messages
            if (Array.isArray(data)) {
                data.forEach((error: any) => {
                    if (error.description) {
                        toast.error(error.description);
                    }
                });
                return;
            }
        }
        
        // Generic error message if response structure is unknown
        toast.error(error.message || "An unexpected error occurred");
    } else {
        toast.error("An unexpected error occurred");
    }
};