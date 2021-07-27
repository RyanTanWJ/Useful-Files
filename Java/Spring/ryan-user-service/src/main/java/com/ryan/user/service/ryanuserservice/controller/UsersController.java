package com.ryan.user.service.ryanuserservice.controller;

import com.ryan.user.service.ryanuserservice.Exceptions.ResourceNotFoundException;
import com.ryan.user.service.ryanuserservice.datastore.document.User;
import com.ryan.user.service.ryanuserservice.datastore.repository.UserRepository;
import com.ryan.user.service.ryanuserservice.model.request.NewUserRequest;
import com.ryan.user.service.ryanuserservice.model.response.GetAllUsersResponse;
import com.ryan.user.service.ryanuserservice.model.response.GetUserResponse;
import com.ryan.user.service.ryanuserservice.model.response.NewUserResponse;
//import org.springframework.data.mongodb.repository.Query;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.server.ResponseStatusException;

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
    public ResponseEntity<GetAllUsersResponse> getAll() {
        try {
            List<User> usersList = userRepository.findAll();
            if (usersList.size() <= 0) {
                throw new ResourceNotFoundException();
            }
            GetAllUsersResponse response = new GetAllUsersResponse(usersList);
            return new ResponseEntity<GetAllUsersResponse>(response, HttpStatus.OK);
        } catch (ResourceNotFoundException exc) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "No Users Found", exc);
        } catch (Exception exc) {
            throw new ResponseStatusException(HttpStatus.INTERNAL_SERVER_ERROR, "Internal Server Error", exc);
        }
    }

    @GetMapping("/{id}")
    public ResponseEntity<GetUserResponse> findUserById(@PathVariable("id") int userId) {
        try {
            Optional<User> foundUser = userRepository.findById(userId);
            if (!foundUser.isPresent()) {
                throw new ResourceNotFoundException();
            }
            GetUserResponse response = new GetUserResponse(foundUser.get());
            return new ResponseEntity<GetUserResponse>(response, HttpStatus.OK);
        } catch (ResourceNotFoundException exc) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "User Not Found", exc);
        } catch (Exception exc) {
            throw new ResponseStatusException(HttpStatus.INTERNAL_SERVER_ERROR, "Internal Server Error", exc);
        }
    }

    @PostMapping("/new")
    public NewUserResponse createNewUser(@RequestBody NewUserRequest newUserRequest) {
        try {
            User newUser = new User(
                    1,
                    newUserRequest.getName(),
                    newUserRequest.getUserName(),
                    System.currentTimeMillis() / 1000L);

            this.userRepository.save(newUser);
            return new NewUserResponse(newUser.getName(), newUser.getUserName());
        } catch (Exception e) {
            throw new ResponseStatusException(HttpStatus.INTERNAL_SERVER_ERROR, "Internal Server Error", e);
        }
    }
}
