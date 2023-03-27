# .NET MAUI application Polyclinic

## Leonenko Aleksei, group 153502

![Class diagram](classes_diagram.png)

## Task description

You are the head of the planning service of a paid polyclinic. Your task is to track the financial performance of the polyclinic.
The polyclinic employs doctors of various specialties with different qualifications. Patients come to the polyclinic every day. All of them undergo mandatory registration, in which standard personal data (surname, first name, patronymic, year of birth) are entered into the database.
Each patient can go to the polyclinic several times, needing various medical care. All appeals of patients are recorded, while the diagnosis is established, the cost of treatment is determined, the date of treatment is remembered.

## Functions of application

### Admin can: ###

* Register clients

* Register doctors

* Edit accounts

* Register appointment

* Decline appointment

* Accept payments

* See appointments

### Doctor can: ###

* See upcoming appointments

* Fill appointment info

### Client can: ###

* Register

* Log in

* Register an appointment

* See electronic prescription

## Data models:
1. `User model` - This model represents a user in the app. It contains such info as user ID, name, email and phone. Is parent for such data models as `Employee`, `Doctor`, `HeadDoctor`, `Administrator` and `Client`

2. `Employee` - This model represents an employee of polyclinics. Child model for `User model`. Contains information about the cabinet where the employee can be found.

3. `Doctor` - This model represents a Doctor. Child model for `Employee model`. Contains info about speciality and info about appointments.

4. `HeadDoctor` - This model represents a Head Doctor.

5. `Administrator` - This model represents an administrator of the polyclinic.

6. `Client` - This model represents a client. Contains medical records info and info about appointments

7. `MedicalRecord` - This model represents a medical conclusion after appointment. Contains an id, an info about a client, a doctor and the 
medical record itself.

8. `Appointment` - This model represents appointment info. It contains an id, a client info, a doctor info, info about date nad time of appointment and prescription.

9. `Prescriprion` - This model represents prescriprion. It contains an id and needed info.
