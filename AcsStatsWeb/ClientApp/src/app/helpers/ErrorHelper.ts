import {ErrorDetails} from "../models/error.model";


export function createError(id: number, message: string | null = null): ErrorDetails {
  return {id: id, message: message}
}
