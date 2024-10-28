import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Student } from '../models/api-models/student.model';
import { updateStudentRequest } from '../models/api-models/updateStudentRequest.model';
import { AddStudentRequest } from '../models/api-models/addStudentRequest.model';

@Injectable({
  providedIn: 'root',
})
export class StudentService {
  private baseApiUrl = 'https://localhost:44300';

  constructor(private httpClient: HttpClient) {}

  getStudents(): Observable<Student[]> {
    return this.httpClient.get<Student[]>(this.baseApiUrl + '/Student');
  }
  getStudent(studentId: string | null): Observable<Student> {
    return this.httpClient.get<Student>(
      this.baseApiUrl + '/Student/' + studentId
    );
  }
  updateStudent(
    studentId: string,
    studentRequest: Student
  ): Observable<Student> {
    const updateStudentRequest: updateStudentRequest = {
      firstName: studentRequest.firstName,
      lastName: studentRequest.lastName,
      dateOfBirth: studentRequest.dateOfBirth,
      email: studentRequest.email,
      mobile: studentRequest.mobile,
      genderId: studentRequest.genderId,
      physicalAdress: studentRequest.adress.physicalAdress,
      postalAdress: studentRequest.adress.postalAdress,
    };
    return this.httpClient.put<Student>(
      this.baseApiUrl + '/Student/' + studentId,
      updateStudentRequest
    );
  }
  deleteStudent(studentId: string): Observable<Student> {
    return this.httpClient.delete<Student>(
      this.baseApiUrl + '/Student/' + studentId
    );
  }
  addStudent(studentRequest: Student): Observable<Student> {
    const addStudentRequest: AddStudentRequest = {
      firstName: studentRequest.firstName,
      lastName: studentRequest.lastName,
      dateOfBirth: studentRequest.dateOfBirth,
      email: studentRequest.email,
      mobile: studentRequest.mobile,
      genderId: studentRequest.genderId,
      physicalAdress: studentRequest.adress.physicalAdress,
      postalAdress: studentRequest.adress.postalAdress,
    };
    return this.httpClient.post<Student>(
      this.baseApiUrl + '/Student/add',
      addStudentRequest
    );
  }
  getImagePath(relativePath: string) {
    return `${this.baseApiUrl}/${relativePath}`;
  }
  uploadImage(studentId: string, file: File): Observable<any> {
    const formData = new FormData();
    formData.append('profileImage', file);
    return this.httpClient.post(
      this.baseApiUrl + '/student/' + studentId + '/uploud-image',
      formData,
      { responseType: 'text' }
    );
  }
}
