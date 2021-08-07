package com.ryan.user.service.ryanuserservice.controller;

import com.ryan.user.service.ryanuserservice.Exceptions.ResourceNotFoundException;
import com.ryan.user.service.ryanuserservice.Exceptions.TestException;
import com.ryan.user.service.ryanuserservice.datastore.document.User;
import com.ryan.user.service.ryanuserservice.datastore.repository.UserRepository;
import com.ryan.user.service.ryanuserservice.model.request.NewUserRequest;
import com.ryan.user.service.ryanuserservice.model.response.*;
//import org.springframework.data.mongodb.repository.Query;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.server.ResponseStatusException;

import java.util.HashMap;
import java.util.List;
import java.util.Optional;

@RestController
@RequestMapping("/users")
public class UsersController {
    private UserRepository userRepository;

    public UsersController(UserRepository userRepository) {
        this.userRepository = userRepository;
    }

    @GetMapping("/all")
    public ResponseEntity getAll() {
        try {
            List<User> usersList = userRepository.findAll();
            if (usersList == null) {
                throw new TestException();
            }
            if (usersList.size() <= 0) {
                throw new ResourceNotFoundException();
            }
            GetAllUsersResponse response = new GetAllUsersResponse(usersList);
            return new ResponseEntity<GetAllUsersResponse>(response, HttpStatus.OK);
        } catch (ResourceNotFoundException exc) {
            ExceptionResponse excResp = new ExceptionResponse(ExceptionCodes.NoUserContent, "No User Content");
            return new ResponseEntity<ExceptionResponse>(excResp, HttpStatus.NO_CONTENT);
        } catch (Exception exc) {
            HashMap<String, Object> metadata = new HashMap<String, Object>();
            metadata.put("exception", exc); // TODO: Putting exception includes entire meta data. Solution this.
            ExceptionResponse excResp = new ExceptionResponse(ExceptionCodes.UnknownError, "Internal Server Error", metadata);
            return new ResponseEntity<ExceptionResponse>(excResp, HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }

    @GetMapping("/{id}")
    public ResponseEntity findUserById(@PathVariable("id") int userId) {
        try {
            Optional<User> foundUser = userRepository.findById(userId);
            if (!foundUser.isPresent()) {
                throw new ResourceNotFoundException();
            }
            GetUserResponse response = new GetUserResponse(foundUser.get());
            return new ResponseEntity<GetUserResponse>(response, HttpStatus.OK);
        } catch (ResourceNotFoundException exc) {
            ExceptionResponse excResp = new ExceptionResponse(ExceptionCodes.UserNotFound, "User Not Found");
            return new ResponseEntity<ExceptionResponse>(excResp, HttpStatus.NO_CONTENT);
        } catch (Exception exc) {
            HashMap<String, Object> metadata = new HashMap<String, Object>();
            metadata.put("exception", exc); // TODO: Putting exception includes entire meta data. Solution this.
            ExceptionResponse excResp = new ExceptionResponse(ExceptionCodes.UnknownError, "Internal Server Error", metadata);
            return new ResponseEntity<ExceptionResponse>(excResp, HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }

    @PostMapping("/new")
    public ResponseEntity createNewUser(@RequestBody NewUserRequest newUserRequest) {
        try {
            User newUser = new User(
                    1,
                    newUserRequest.getName(),
                    newUserRequest.getUserName(),
                    System.currentTimeMillis() / 1000L);

            this.userRepository.save(newUser);
            NewUserResponse response = new NewUserResponse(newUser.getName(), newUser.getUserName());
            return new ResponseEntity<NewUserResponse>(response, HttpStatus.OK);
        } catch (Exception e) {
            HashMap<String, Object> metadata = new HashMap<String, Object>();
            metadata.put("exception", e); // TODO: Putting exception includes entire meta data. Solution this.
            ExceptionResponse excResp = new ExceptionResponse(ExceptionCodes.UnknownError, "Internal Server Error", metadata);
            return new ResponseEntity<ExceptionResponse>(excResp, HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }
}
